using OmahaMtg.Data;
using System;
using System.Linq;

namespace OmahaMtg.Users
{
    public class UserManager : IUserManager
    {
        public string GetUserEmail(Guid userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                return user?.Email;
            }
        }

        public string GetUserFullname(Guid userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                return $"{user?.FirstName} {user?.LastName}";
            }
        }

        public void UpdateLastSignInTime(string email)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email || u.UserName == email);
                if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public int CleanupUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var twoDaysAgo = DateTime.Now.AddDays(-2);
                var NonVerifiedAccounts = context.Users.Where(w => !w.EmailConfirmed && w.CreateDate < twoDaysAgo);

                foreach (var user in NonVerifiedAccounts)
                {
                    context.Users.Remove(user);
                    
                }
                context.SaveChanges();

                return NonVerifiedAccounts.Count();
                //var NoLoginOverLastYear = context.Users.Where(w => w.LastLogin == null ||)
                //if (user != null)
                //{
                //    user.LastLogin = DateTime.Now;
                //    context.SaveChanges();
                //}
            }
        }
    }
}
