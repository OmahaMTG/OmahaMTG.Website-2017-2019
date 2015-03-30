using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OmahaMtg.Email;

namespace OmahaMtg
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var smtpFrom = System.Configuration.ConfigurationManager.AppSettings["smtpFrom"];

            IEmailer emailer = new Emailer();


            EmailInfo identityMessage = new EmailInfo()
            {
                From = smtpFrom,
                TextBody = message.Body,
                Subject = message.Subject,
            };

            identityMessage.To.Add(message.Destination);
            return emailer.SendEmailAsync(identityMessage);
        }
    }
}
