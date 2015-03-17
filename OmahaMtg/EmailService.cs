using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OmahaMtg
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:

            var smtpUserName = System.Configuration.ConfigurationManager.AppSettings["smtpUserName"];
            var smtpServer = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
            var smtpPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"];
            var smtpFrom = System.Configuration.ConfigurationManager.AppSettings["smtpFrom"];

            // Configure the client:
            var client =
                new System.Net.Mail.SmtpClient(smtpServer, Convert.ToInt32(587));

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(smtpUserName, smtpPassword);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(smtpFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}
