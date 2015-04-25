using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Events
{
    public class EventInfo 
    {
        public EventInfo()
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

        public DateTime? EventStartTime { get; set; }
        public DateTime? EventEndTime { get; set; }

        public bool IsUserRsvpd { get; set; }
        public int TotalRsvpCount { get; set; }
        public long? VimeoId { get; set; }
        

    }
}
