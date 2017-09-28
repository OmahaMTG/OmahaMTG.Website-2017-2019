using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Threading.Tasks;

namespace OmahaMtg.Email
{
    public class Emailer : IEmailer
    {
        public string EmailUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["smtpUserName"];
            }
        }

        public string EmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["smtpPassword"];
            }
        }

        public string SiteEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["siteEmail"];
            }
        }

        public async Task SendEmailAsync(EmailInfo emailInfo)
        {
            if (emailInfo.From == null)
                emailInfo.From = SiteEmail;

            var myMessage =
                new MailMessage();

            myMessage.Body = emailInfo.HtmlBody ?? emailInfo.TextBody;
            myMessage.Subject = emailInfo.Subject;
            myMessage.IsBodyHtml = true;
            myMessage.From = new MailAddress(emailInfo.From, emailInfo.FromName);

            foreach (var address in emailInfo.To)
            {
                myMessage.To.Add(new MailAddress(address));
            }

            using (var client = new SmtpClient()) // SmtpClient configuration comes from config file
            {
                OmahaMtg.Log.Logging.Information("Sending Email: {@MailMessage}", myMessage);

                try
                {
                    await client.SendMailAsync(myMessage);
                }
                catch (Exception ex)
                {
                     OmahaMtg.Log.Logging.Error(ex, "Error Sending Email");
                }
                

            }
        }
    }
}