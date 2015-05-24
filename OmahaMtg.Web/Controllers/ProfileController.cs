using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OmahaMtg.Profile;

namespace OmahaMtg.Web.Controllers
{
    public class ProfileController : Controller
    {
        IProfileManager profileManager { get; set; }

        public ProfileController()
        {
            profileManager = new ProfileManager();
        }
        // GET: Profile
        public ActionResult Index()
        {
            var profile = profileManager.GetUserProfile(new Guid(User.Identity.GetUserId()));
            var model = new Models.ProfileViewModels.User();

            model.EmailAddress = profile.Email;
            model.FirstName = profile.FirstName;
            model.LastName = profile.LastName;
            model.GitHubUser = profile.GitHubUser;
            model.WebsiteUrl = profile.WebsiteUrl;
            model.TwitterUser = profile.TwitterUser;

            model.AvailableGroups = profile.AvailableGroups;
            model.UsersGroups = profile.UsersGroups.ToList();
            
            return View(model);
        }

        [HttpPost]
        public JsonResult UpdateProfile(Models.ProfileViewModels.User user)
        {
            var userId = new Guid(User.Identity.GetUserId());

            profileManager.UpdateProfile(userId, new ProfileInfo()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                GitHubUser = user.GitHubUser,
                TwitterUser = user.TwitterUser,
                WebsiteUrl = user.WebsiteUrl,
                Email = user.EmailAddress, UsersGroups = user.UsersGroups
            });

            return Json(true);

        }

        [HttpPost]
        public JsonResult UpdatePassword(Models.ProfileViewModels.UpdatePassword updatePassword)
        {
            var userId = new Guid(User.Identity.GetUserId());

            if (!string.Equals(updatePassword.NewPassword, updatePassword.ConfirmNewPassword, StringComparison.Ordinal))
            {
                return Json(new OmahaMtg.Web.Models.ProfileViewModels.UpdatePasswordResult()
                {
                    Result = false,
                    ErrorMessage = "Confirmed password does not match new password"
                });
            }

            var result = profileManager.UpdatePassword(userId, updatePassword.OldPassword, updatePassword.NewPassword);

            return Json(new OmahaMtg.Web.Models.ProfileViewModels.UpdatePasswordResult()
            {
                Result = result.Item1,
                ErrorMessage = result.Item2
            });

        } 
    }
}