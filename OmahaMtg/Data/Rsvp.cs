using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    class Rsvp
    {
        public Guid UserId { get; set; }
        public int EventId { get; set; }
        public DateTime RsvpTime { get; set; }

        //public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
