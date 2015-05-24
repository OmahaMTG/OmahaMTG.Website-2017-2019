using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmahaMtg.Web.Models.ProfileViewModels
{
    public class User
    {
        public User()
        {
            UsersGroups = new List<int>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterUser { get; set; }
        public string GitHubUser { get; set; }
        public string EmailAddress { get; set; }

        public Dictionary<int, string> AvailableGroups { get; set; }
        public IEnumerable<int> UsersGroups { get; set; } 
    }

    public class UpdatePassword
    {
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
        public String ConfirmNewPassword { get; set; }
    }

    public class UpdatePasswordResult
    {
        public string ErrorMessage { get; set; }
        public bool Result { get; set; }

    }
}