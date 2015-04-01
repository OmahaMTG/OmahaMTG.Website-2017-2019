using System;
using System.Collections.Generic;
using System.Configuration;
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

        public Task SendEmailAsync(EmailInfo emailInfo)
        {
            var myMessage = new SendGridMessage();

            if (emailInfo.From == null)
                emailInfo.From = SiteEmail;

            // Add the message properties.
            myMessage.From = new MailAddress(emailInfo.From);

            myMessage.AddTo(emailInfo.To);
            
            foreach (var address in emailInfo.Bcc)
            {
                myMessage.AddBcc(address);
            }

            myMessage.Subject = emailInfo.Subject;

            //Add the HTML and Text bodies
            if(!string.IsNullOrEmpty(emailInfo.TextBody))
                myMessage.Text = emailInfo.TextBody;

            if (!string.IsNullOrEmpty(emailInfo.HtmlBody))
                myMessage.Html = emailInfo.HtmlBody;

            var credentials = new NetworkCredential(EmailUserName, EmailPassword);

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            return transportWeb.DeliverAsync(myMessage);
        }
    }
}