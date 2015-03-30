using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OmahaMtg.Email
{
    public class EmailInfo 
    {
        public EmailInfo()
        {
            To = new List<string>();
            Bcc = new List<string>();
        }



        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public List<string> To { get; set; }
        public string From { get; set; }
        public List<string> Bcc { get; set; }
    }
}
