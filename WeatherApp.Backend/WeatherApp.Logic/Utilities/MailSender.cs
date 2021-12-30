using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Mail;
using WeatherApp.Data.Helpers;
using WeatherApp.Logic.Services;

namespace WeatherApp.Logic.Utilities
{
    public interface IMailSender
    {
        public void SendEmailAsync(string email);
    }

    public class MailSender : IMailSender
    {
        private readonly MailSettings _mailSettings;
        private readonly UserCrudService _userService;
        private readonly AssetsService _assetsService;

        public MailSender(IOptions<MailSettings> mailSettings, UserCrudService userCrudService, AssetsService assetsService)
        {
            _mailSettings = mailSettings.Value;
            _userService = userCrudService;
            _assetsService = assetsService;
        }

        public void SendEmailAsync(string email)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("misterhipster552@gmail.com", "Danieldona2");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            string body;
            using (var sr = new StreamReader("C:/Users/predrag.nikolikj/source/repos/WeatherApp/WeatherApp.Backend/WeatherApp.Logic/Template/Mailtemplate.html"))
            {
                body = sr.ReadToEnd();
            }

            var curentUser = _userService.GetUserByEmail(email);
            var curentAsset = _assetsService.GetAssets(curentUser);

            string message = string.Format(body, curentUser.Username, curentAsset.Guid);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("misterhipster552@gmail.com", "Weather App");
            mail.Subject = "Mail Activation";
            mail.To.Add(new MailAddress(email));
            mail.IsBodyHtml = true;
            mail.Body = message;

            smtpClient.Send(mail);
        }
    }
}
