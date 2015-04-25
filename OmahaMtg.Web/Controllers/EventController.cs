using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using OmahaMtg.Events;
using OmahaMtg.Web.Models.PostViewModels;

namespace OmahaMtg.Web.Controllers
{
    public class EventController : Controller
    {
        private OmahaMtg.Events.IEventManager _em;
        public EventController()
        {
            _em = new EventManager();
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_em.GetEvent(id, new Guid(User.Identity.GetUserId())));
            }
            else
            {
                return View(_em.GetEvent(id));
            }
        }

        [HttpPost]
        public JsonResult RsvpForEvent(RsvpForEventViewModel model)
        {
            return Json(_em.UpdateRsvp(new Guid(User.Identity.GetUserId()), model.EventId, model.UserIsGoing));
        }

        public ActionResult UpcomingEvents()
        {
            return View(_em.GetUpcomingEvents(5));
        }

        public ActionResult RecentVideos()
        {
            return View(_em.GetLatestEventsWithVideos(5));
        }

    }
}
