using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using OmahaMtg.Email;
using OmahaMtg.Events;
using OmahaMtg.Groups;
using OmahaMtg.Events;
using OmahaMtg.Profile;

using ModelBinderAttribute = System.Web.Http.ModelBinding.ModelBinderAttribute;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    public class EmailController : Controller
    {
        private IEventManager _pm;
        private IProfileManager _profileManager;
        private IGroupManager _gm;
        public EmailController()
        {
            _pm = new EventManager();
            _gm = new GroupManager();
            _profileManager = new ProfileManager();
        }

        public string SiteEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["siteEmail"];
            }
        }



        // GET: Admin/Email
        public ActionResult Index(int ? eventId)
        {
            var model = new Models.Email.Email();
            model.AvailableGroups = _pm.GetAvailableGroups();
            model.RecipientGroups = new List<int>();

            
            model.FromEmail = SiteEmail;
            model.SendAsTest = true;
            //if (eventId.HasValue)
            //{
            //    var post = _pm.GetPost(eventId.Value, userId);
            //    model.Body = post.Body;
            //}

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SendEmail(Models.Email.Email model)
        {
            // Credentials:
            IEmailer emailer = new Emailer();

            var message = new EmailInfo()
            {
                From = model.FromEmail,
                Subject = model.Subject,
                HtmlBody = MarkdownService.GetHtmlFromMarkdown(model.Body),
               
            };
            message.To.Add(SiteEmail);

            int count = 0;

            if (!model.SendAsTest)
            {
                foreach (var group in model.RecipientGroups)
                {
                    var emails = _gm.GetUserEmailsInGroup(group);

                    var emailTasks = emails.Select(to =>
                    {
                        count++;
                        message.To.Clear();
                        message.To.Add(to);
                        return emailer.SendEmailAsync(message);
                    });

                    await Task.WhenAll(emailTasks);
                }
                return Json(count);
            }
            else
            {
                var userId = new Guid(User.Identity.GetUserId());
                message.To.Clear();
                message.To.Add(_profileManager.GetUserProfile(userId).Email);
                await emailer.SendEmailAsync(message);


                return Json(1);
            }
 
        }

    }
}