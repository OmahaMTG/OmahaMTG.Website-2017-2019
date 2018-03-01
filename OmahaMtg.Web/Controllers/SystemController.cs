using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OmahaMtg.Users;

namespace OmahaMtg.Web.Controllers
{
    public class SystemController : Controller
    {
        private IUserManager _userManager;

        public SystemController()
        {
            _userManager = new UserManager();
        }

        [System.Web.Http.HttpGet]
        public JsonResult CleanupUsers()
        {
            return Json(_userManager.CleanupUsers());
        }
    }
}
