using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            HasRequired(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId);


            HasMany(e => e.Groups)
                .WithMany(e => e.Posts)
                .Map(m=> m.MapLeftKey("PostId")
                    .MapRightKey("GroupId"));
        }
    }
}
