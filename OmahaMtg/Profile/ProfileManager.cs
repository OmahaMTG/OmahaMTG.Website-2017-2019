using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OmahaMtg.Data;

namespace OmahaMtg.Profile
{
    public class ProfileManager : IProfileManager
    {
        private ApplicationUserManager _userManager;
        public ProfileManager()
        {
            _userManager = new ApplicationUserManager(new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(new ApplicationDbContext()));
        }

        public ProfileInfo GetUserProfile(Guid userId)
        {
            return MapUserToProfileInfo(GetUser(userId));
        }

        public void UpdateProfile(Guid userId, ProfileInfo profile)
        {
            var user = GetUser(userId);

            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.GitHubUser = profile.GitHubUser;
            user.WebsiteUrl = profile.WebsiteUrl;
            user.TwitterUser = profile.TwitterUser;
            user.Email = profile.Email;
            _userManager.Update(user);
        }


        public Tuple<bool, string> UpdatePassword(Guid userId, string currentPassword, string newPassword)
        {
            var changePasswordResult = _userManager.ChangePassword(userId, currentPassword, newPassword);
            return new Tuple<bool, string>(changePasswordResult.Succeeded, string.Join(",", changePasswordResult.Errors));
        }

        private User GetUser(Guid userId)
        {
            return _userManager.Users.FirstOrDefault(w => w.Id == userId);
        }

        private ProfileInfo MapUserToProfileInfo(User user)
        {
            return new ProfileInfo()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                GitHubUser = user.GitHubUser,
                TwitterUser = user.TwitterUser,
                WebsiteUrl = user.WebsiteUrl
            };
        }


        public void UpdateEmail(Guid userId, string email)
        {
            throw new NotImplementedException();
        }

        public void ConfirmUpdatedEmail(Guid userId, Guid newEmailId)
        {
            throw new NotImplementedException();
        }
    }
}
