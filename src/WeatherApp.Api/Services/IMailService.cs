using WeatherApp.Api.Helpers;
using System.Threading.Tasks;

namespace WeatherApp.Api.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}