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
        public async Task<JsonResult> SendEmail(Models.Email.Email model)
        {
            // Credentials:
            IEmailer emailer = new Emailer();

            var message = new EmailInfo()
            {
                From = model.FromEmail,
                Subject = model.Subject,
                TextBody = model.Body,
               
            };
            message.To.Add(SiteEmail);

            if (!model.SendAsTest)
            {
                foreach (var group in model.RecipientGroups)
                {
                    foreach (var email in _gm.GetUserEmailsInGroup(group))
                    {
                        message.Bcc.Add(email);
                    }
                }
            }
            else
            {
                message.Bcc.Add(model.FromEmail);
            }


            try
            {
                await emailer.SendEmailAsync(message);
                return Json(message.Bcc.Count);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", ex.Message);
                return Json(error);
            }


            
        }

       
    }
}