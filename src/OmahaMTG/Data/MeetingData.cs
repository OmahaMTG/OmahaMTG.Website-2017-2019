using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("Meetings")]
    class MeetingData : DataEntityBase
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public IEnumerable<PresentationData> Presentations { get; set; }
        public HostData MeetingHost { get; set; } 
        public int? MeetingHostId { get; set; }
        public IEnumerable<MeetingSponsorData> MeetingSponsors { get; set; }
        public long? VimeoId { get; set; }
        public IEnumerable<MeetingTagData> MeetingTags { get; set; }
    }
}
