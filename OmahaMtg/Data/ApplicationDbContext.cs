using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OmahaMtg.Data
{
    class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Configurations.Add(new RsvpConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Entity<Event>().ToTable("Events");

            base.OnModelCreating(modelBuilder);
        }

        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Rsvp> Rsvps { get; set; }
        public System.Data.Entity.DbSet<Event> Events { get; set; }
        public System.Data.Entity.DbSet<Group> Groups { get; set; }
        /// <summary>
        /// Gets or sets the banner adds.
        /// </summary>
        /// <value>
        /// The banner adds.
        /// </value>
        public System.Data.Entity.DbSet<BannerAdd> BannerAdds { get; set; }
    }
}
