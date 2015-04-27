using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmahaMtg.Events;

namespace OmahaMtg.Web.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    OmahaMtg.Posts.IPostManager pm = new PostManager();

        //    return Index(1);
        //}

        public ActionResult Index(int page =1)
        {
            OmahaMtg.Events.IEventManager pm = new EventManager();

            int skipCount = 0;

            skipCount = (page - 1)*10;


            return View(pm.GetEvents(skipCount, 10, false, false, false));
        }

        
    }
}