using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmahaMtg.Web.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Contribute()
        {
            return View();
        }

        // GET: Pages
        public ActionResult Sponsors()
        {
            return View();
        }

        // GET: Pages
        public ActionResult About()
        {
            return View();
        }

        public ActionResult CodeOfConduct()
        {
            return View();
        }
    }
}