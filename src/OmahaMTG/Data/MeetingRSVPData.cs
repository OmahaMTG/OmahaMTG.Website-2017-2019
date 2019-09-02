using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("MeetingRSVPs")]
    class MeetingRsvpData
    {
        public string UserId { get; set; }

        public int MeetingId { get; set; }
        public MeetingData Meeting { get; set; }
    }
}
