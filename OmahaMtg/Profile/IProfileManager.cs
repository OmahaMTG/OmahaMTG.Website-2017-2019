using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;


namespace OmahaMtg.Profile
{
    public interface IProfileManager
    {
        ProfileInfo GetUserProfile(Guid userId);
        void UpdateProfile(Guid userId, ProfileInfo profile);
        Tuple<bool, string> UpdatePassword(Guid userId, string currentPassword, string newPassword);
        void UpdateEmail(Guid userId, string email);
        void ConfirmUpdatedEmail(Guid userId, Guid newEmailId);
    }
}
