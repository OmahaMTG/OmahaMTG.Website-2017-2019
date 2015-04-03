using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Posts
{
    public class PostInfo
    {
        public PostInfo()
        {
            AvailableGroups = new Dictionary<int, string>();
            GroupTags = new List<int>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<int> GroupTags { get; set; }
        public Dictionary<int, string> AvailableGroups { get; set; }
        public string HtmlBody { get; set; }
        public string HtmlBodySummary { get; set; }



        public Guid CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }

     
        public DateTime? PublishStartTime { get; set; }
        public DateTime? PublishEndTime { get; set; }
        public bool IsDeleted { get; set; }
     

    }
}
