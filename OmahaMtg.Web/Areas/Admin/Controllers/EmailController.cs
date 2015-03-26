using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using OmahaMtg.Groups;
using OmahaMtg.Posts;
using OmahaMtg.Profile;
using ModelBinderAttribute = System.Web.Http.ModelBinding.ModelBinderAttribute;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    public class EmailController : Controller
    {
        private IPostManager _pm;
        private IProfileManager _profileManager;
        private IGroupManager _gm;
        public EmailController()
        {
            _pm = new PostManager();
            _gm = new GroupManager();
            _profileManager = new ProfileManager();
        }


        // GET: Admin/Email
        public ActionResult Index(int ? eventId)
        {
            var model = new Models.Email.Email();
            model.AvailableGroups = _pm.GetAvailableGroups();
            model.RecipientGroups = new List<int>();

            var userId = new Guid(User.Identity.GetUserId());
            model.FromEmail = _profileManager.GetUserProfile(userId).Email;
            model.SendAsTest = true;
            //if (eventId.HasValue)
            //{
            //    var post = _pm.GetPost(eventId.Value, userId);
            //    model.Body = post.Body;
            //}

            return View(model);
        }

        [HttpPost]
        public JsonResult SendEmail(Models.Email.Email model)
        {

            //OmahaMtg.Posts.IPostManager pm = new PostManager();
            //return Json(pm.UpdateRsvp(new Guid(User.Identity.GetUserId()), model.EventId, model.UserIsGoing));
            //throw new NotImplementedException();

            // Credentials:

            var smtpUserName = System.Configuration.ConfigurationManager.AppSettings["smtpUserName"];
            var smtpServer = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
            var smtpPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"];
            var smtpFrom = model.FromEmail;

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
                new System.Net.Mail.MailMessage(smtpFrom, smtpFrom);

            if (!model.SendAsTest)
            {
                foreach (var group in model.RecipientGroups)
                {
                    foreach (var email in _gm.GetUserEmailsInGroup(group))
                    {
                        mail.Bcc.Add(email);
                    }
                }
            }

            mail.Subject =model.Subject;
            mail.Body = model.Body;

            // Send:
            client.Send(mail);

            return Json(mail.Bcc.Count);
        }

       
    }
}