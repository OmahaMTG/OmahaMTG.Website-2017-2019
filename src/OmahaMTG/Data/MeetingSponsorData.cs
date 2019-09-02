using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("Meeting_Sponsors")]
    class MeetingSponsorData
    {
        public int MeetingId { get; set; }
        public MeetingData Meeting { get; set; }
        public int SponsorId { get; set; }
        public SponsorData Sponsor { get; set; }
    }
}
