using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OmahaMtg.Data
{
    class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Configurations.Add(new RsvpConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Post>().ToTable("Posts");

            base.OnModelCreating(modelBuilder);
        }

        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Event> Events { get; set; }
        public System.Data.Entity.DbSet<Rsvp> Rsvps { get; set; }
        public System.Data.Entity.DbSet<Post> Posts { get; set; }
        public System.Data.Entity.DbSet<Group> Groups { get; set; }


    }
}
