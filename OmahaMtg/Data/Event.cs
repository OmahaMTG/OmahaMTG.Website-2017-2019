﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    public class Event
    {
        public Event()
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
        public Guid CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual IList<Group> Groups { get; set; }
 
        public DateTime? EventStartTime { get; set; }
        public DateTime? EventEndTime { get; set; }
        public string Location { get; set; }
        public string Sponsor { get; set; }
        public long? VimeoId { get; set; }
    }
}
