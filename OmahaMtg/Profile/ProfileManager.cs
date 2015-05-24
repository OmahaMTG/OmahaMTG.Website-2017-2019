using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using OmahaMtg.Data;

namespace OmahaMtg.Profile
{
    public class ProfileManager : IProfileManager
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _dbContext;
        public ProfileManager()
        {
            _dbContext = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(_dbContext));
        }

        public ProfileInfo GetUserProfile(Guid userId)
        {
            var profile = MapUserToProfileInfo(GetUser(userId));
            profile.AvailableGroups = GetGroups();
            profile.UsersGroups = GetUsersGroups(userId).ToList();

            return profile;
        }

        public Dictionary<int, string> GetGroups()
        {
            return _dbContext.Groups.ToDictionary(s => s.Id, s => s.Name);

        }

        public IEnumerable<int> GetUsersGroups(Guid userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == userId).Groups.Select(s => s.Id);
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
            UpdateGroupMembership(userId, profile.UsersGroups.ToList());
        }

        public void UpdateGroupMembership(Guid userId, List<int> groupIds)
        {
            var updateUser = _dbContext.Users.Include("Groups").FirstOrDefault(w => w.Id == userId);

            var groups = _dbContext.Groups.ToList();
            
            if (updateUser == null)
                return;

            updateUser.Groups.Clear();
            foreach (var id in groupIds)
            {
                updateUser.Groups.Add(groups.FirstOrDefault(w => w.Id == id));
            }

            _dbContext.SaveChanges();
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
