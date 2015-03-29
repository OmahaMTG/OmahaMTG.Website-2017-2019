using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Threading.Tasks;

namespace OmahaMtg.Email
{
    public class Emailer : IEmailer
    {

        public Task SendEmailAsync(EmailInfo emailInfo)
        {
            var myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress(emailInfo.From);

            // Add multiple addresses to the To field.
            

            myMessage.AddTo(emailInfo.From);

            foreach (var address in emailInfo.Bcc)
            {
                myMessage.AddBcc(address);
            }
            

            myMessage.Subject = emailInfo.Subject;

            //Add the HTML and Text bodies
            myMessage.Text = emailInfo.TextBody;
            myMessage.Html = emailInfo.HtmlBody;

            var credentials = new NetworkCredential("username", "password");

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.DeliverAsync(myMessage);
        }
    }
}