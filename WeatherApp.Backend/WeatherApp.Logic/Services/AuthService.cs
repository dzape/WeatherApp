namespace WeatherApp.Logic.Services
{
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using WeatherApp.Data.DataContext;
    using WeatherApp.Data.Entities;
    using WeatherApp.Data.Helpers;
    using BC = BCrypt.Net.BCrypt;

    public class AuthService
    {
        private readonly DatabaseContext _context;
        private readonly AssetsService _userAssets;

        public AuthService(DatabaseContext context, AssetsService userAssets)
        {
            _context = context;
            _userAssets = userAssets;
        }

        public bool Authenticate(UserLogin user)
        {
            var acc = _context.Users.FirstOrDefault(x => x.Username == user.Username);

            if (user == null || !BC.Verify(user.Password, acc.Password))
            {
                return false;
            }
            return true;
        }

        public string GenerateToken(User user)
        {
            var assets = _userAssets.GetAssets(user);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("superSecretKey@345");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                    new Claim(ClaimTypes.Role, assets.Role.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
