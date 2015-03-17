using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    class RsvpConfiguration : EntityTypeConfiguration<Rsvp>
    {
        public RsvpConfiguration()
        {

            HasKey(pc => new { pc.UserId, pc.EventId });
        }
    }
}
