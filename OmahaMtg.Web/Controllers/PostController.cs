using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmahaMtg.Posts;
using Microsoft.AspNet.Identity;
using OmahaMtg.Web.Models.PostViewModels;

namespace OmahaMtg.Web.Controllers
{
    public class PostController : Controller
    {

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            OmahaMtg.Posts.IPostManager pm = new PostManager();

            if (User.Identity.IsAuthenticated)
            {
                return View(pm.GetPost(id, new Guid(User.Identity.GetUserId())));
            }
            else
            {
                return View(pm.GetPost(id));
            }
        }

        [HttpPost]
        public JsonResult RsvpForEvent(RsvpForEventViewModel model)
        {

            OmahaMtg.Posts.IPostManager pm = new PostManager();
            return Json(pm.UpdateRsvp(new Guid(User.Identity.GetUserId()), model.EventId, model.UserIsGoing));

        }

        public ActionResult UpcomingEvents()
        {
            OmahaMtg.Posts.IPostManager pm = new PostManager();
            return View(pm.GetUpcomingEvents(5));

        }

        public ActionResult RecentVideos()
        {
            OmahaMtg.Posts.IPostManager pm = new PostManager();
            return View(pm.GetLatestEventsWithVideos(5));
        }

    }
}
