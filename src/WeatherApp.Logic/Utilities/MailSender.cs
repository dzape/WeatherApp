using MimeKit;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WeatherApp.Data.Helpers;
using System.Net.Mail;

namespace WeatherApp.Logic.Utilities
{
    public interface IMailSender
    {
        public void SendEmailAsync(MailRequest mailRequest);
    }

    public class MailSender : IMailSender
    {
        private readonly MailSettings _mailSettings;

        public MailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendEmailAsync(MailRequest mailRequest)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential("yourmail@gmail.com", "yourpass");
            smtpClient.UseDefaultCredentials = false; 
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("yourmail@gmail.com", "Weather App");
            mail.To.Add(new MailAddress(mailRequest.ToEmail));
            
            smtpClient.Send(mail);
        }
    }
}
