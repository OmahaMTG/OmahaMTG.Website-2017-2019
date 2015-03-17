using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmahaMtg.Posts;
using ModelBinderAttribute = System.Web.Http.ModelBinding.ModelBinderAttribute;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    public class EmailController : Controller
    {
        private IPostManager _pm;
        public EmailController()
        {
            _pm = new PostManager();
            
        }


        // GET: Admin/Email
        public ActionResult Index(int ? eventId)
        {
            var model = new Models.Email.Email();
            model.AvailableGroups = _pm.GetAvailableGroups();
            model.RecipientGroups = new List<int>();

            return View(model);
        }

        [HttpPost]
        public JsonResult SendEmail(Models.Email.Email model)
        {

            //OmahaMtg.Posts.IPostManager pm = new PostManager();
            //return Json(pm.UpdateRsvp(new Guid(User.Identity.GetUserId()), model.EventId, model.UserIsGoing));
            throw new NotImplementedException();
        }
    }
}