using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmahaMtg.Posts;

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
            OmahaMtg.Posts.IPostManager pm = new PostManager();

            int skipCount = 0;

            skipCount = (page - 1)*10;


            return View(pm.GetPosts(skipCount, 10, false));
        }

        
    }
}