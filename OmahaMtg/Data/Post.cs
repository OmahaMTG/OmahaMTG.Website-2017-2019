using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    public class Post
    {
        public Post()
        {
            var currentDate = DateTime.UtcNow;
            CreatedTime = currentDate;
            ModifiedTime = currentDate;
            IsDeleted = false;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? PublishEndTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual IList<Group> Groups { get; set; }
 
    }
}
