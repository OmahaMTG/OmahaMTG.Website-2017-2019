using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            HasRequired(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId);


            HasMany(e => e.Groups)
                .WithMany(e => e.Events)
                .Map(m=> m.MapLeftKey("EventId")
                    .MapRightKey("GroupId"));
        }
    }
}
