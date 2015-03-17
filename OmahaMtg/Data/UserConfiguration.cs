using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {

            HasMany(e => e.Groups)
            .WithMany(e => e.Users)
            .Map(m => m.MapLeftKey("UserId")
                .MapRightKey("GroupId"));
            
        }
    }
}
