using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OmahaMtg.Posts;
using OmahaMtg.Posts.Report;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        private OmahaMtg.Posts.IPostManager _pm;
        public EventController()
        {
            _pm = new PostManager();



        }
        // GET: Admin/Event
        public ActionResult Index(int page = 1)
        {
            int skip = (page - 1)*10;
            return View( _pm.GetPosts(skip, 10, true));
        }

        public ActionResult Details(int id)
        {
            return View(_pm.GetPost(id));
        }

        public ActionResult New()
        {
            var emptyEvent = new EventInfo();
            emptyEvent.AvailableGroups = _pm.GetAvailableGroups();
            return View("Details", emptyEvent);
        }

        [HttpPost]
        public ActionResult New(EventInfo post)
        {
            post.CreatedByUserId = User.Identity.GetUserId();

            int newEventId = _pm.CreateEvent(post);

            return RedirectToAction("Details", "Event", new { id = newEventId });
        }


        [HttpPost]
        public ActionResult Details(EventInfo post)
        {
            
            _pm.UpdateEvent(post);
          


            return RedirectToAction("Details", "Event", new { id = post.Id });
        }

        public FileResult Download(int eventId)
        {
            IEventReport report = new EventReport();

            byte[] fileBytes = report.GetEventReport(eventId);
            string fileName = "EventReport.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}