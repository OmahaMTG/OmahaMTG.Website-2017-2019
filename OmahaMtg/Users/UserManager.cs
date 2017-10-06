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
    }
}
