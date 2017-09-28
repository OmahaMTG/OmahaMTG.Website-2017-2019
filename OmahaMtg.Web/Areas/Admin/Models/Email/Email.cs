using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace OmahaMtg.Web.Areas.Admin.Models.Email
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        

        public List<int> RecipientGroups { get; set; }
        public Dictionary<int, string> AvailableGroups { get; set; }

        public bool SendAsTest { get; set; }
    }
}