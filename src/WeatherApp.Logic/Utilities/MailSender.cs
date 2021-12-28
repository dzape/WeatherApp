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

            smtpClient.Credentials = new System.Net.NetworkCredential("@gmail.com", "pass");
            //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("@gmail.com", "MyWeb Site");
            mail.To.Add(new MailAddress(mailRequest.ToEmail));

            smtpClient.Send(mail);
        }
    }
}
