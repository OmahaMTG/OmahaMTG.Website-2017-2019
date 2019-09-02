using System;
using System.Collections.Generic;

namespace OmahaMTG.AdminContentHandlers.Meeting
{
    public class Model
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }
        public int RsvpCount { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public long? VimeoId { get; set; }
        public int? HostId { get; set; }
        public IEnumerable<int> SponsorIds { get; set; }
        public IEnumerable<int> PresentationIds { get; set; }
    }
}