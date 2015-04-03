using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    public class Event : Post
    {
        public DateTime? EventStartTime { get; set; }
        public DateTime? EventEndTime { get; set; }
        public string Location { get; set; }
        public string Sponsor { get; set; }
        public long? VimeoId { get; set; }
    }
}
