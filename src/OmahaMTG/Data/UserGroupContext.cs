using Microsoft.EntityFrameworkCore;

namespace OmahaMTG.Data
{

    class UserGroupContext : DbContext
    {
        //private readonly ITimeUtility _timeUtility;
        //public UserGroupContext(DbContextOptions<UserGroupContext> options/*, ITimeUtility timeUtility*/)
        //: base(options, )
        //{
        //    //_timeUtility = timeUtility;
        //}



        public DbSet<MeetingData> Meetings { get; set; }
        public DbSet<HostData> Hosts { get; set; }
        public DbSet<SponsorData> Sponsors { get; set; }
        public DbSet<PresentationData> Presentations { get; set; }
        public DbSet<PresenterData> Presenters { get; set; }
        public DbSet<MeetingSponsorData> MeetingSponsors { get; set; }
        public DbSet<PostData> Posts { get; set; }
        public DbSet<PresentationPresenterData> PresentationPresenters { get; set; }
        public DbSet<TagData> Tags { get; set; }
        public DbSet<MeetingRsvpData> MeetingRsvps { get; set; }
        public DbSet<MeetingTagData> MeetingsTags { get; set; }

        public DbSet<PostTagData> PostTags { get; set; }

        public DbSet<SiteSponsorData> SiteSponsors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeetingSponsorData>()
                .HasKey(es => new { es.MeetingId, es.SponsorId });

            modelBuilder.Entity<PresentationPresenterData>()
                .HasKey(pp => new { pp.PresentationId, pp.PresenterId });

            modelBuilder.Entity<MeetingRsvpData>()
                .HasKey(er => new { er.MeetingId, er.UserId });

            modelBuilder.Entity<MeetingTagData>()
                .HasKey(er => new { er.MeetingId, er.TagId });

            modelBuilder.Entity<PostTagData>()
                .HasKey(er => new { er.PostId, er.TagId });
        }

       // public int CurrentUserId { get; set; }



        //public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        //{
        //    var modifiedEntries = ChangeTracker.Entries()
        //        .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

        //    var identityUser = await Users.FirstOrDefaultAsync(u => u.Id == CurrentUserId, cancellationToken: cancellationToken);

        //    //var now = _timeUtility.CurrentTime;

        //    //foreach (var entry in modifiedEntries)
        //    //{
        //    //    if (entry.Entity is DataEntityBase entity)
        //    //    {
        //    //        if (entry.State == EntityState.Added)
        //    //        {
        //    //            entity.CreatedBy = identityUser?.UserName ?? "unknown";
        //    //            entity.CreatedDate = now;
        //    //        }

        //    //        entity.UpdatedBy = identityUser?.UserName ?? "unknown";
        //    //        entity.UpdatedDate = now;
        //    //    }
        //    //}

        //    return await  base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}

        //public override int SaveChanges()
        //{
        //    var modifiedEntries = ChangeTracker.Entries()
        //        .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

        //    var identityUser = Users.FirstOrDefault(u => u.Id == CurrentUserId);
            
        //    //var now = _timeUtility.CurrentTime;

        //    //foreach (var entry in modifiedEntries)
        //    //{
        //    //    if (entry.Entity is DataEntityBase entity)
        //    //    {
        //    //        if (entry.State == EntityState.Added)
        //    //        {
        //    //                entity.CreatedBy = identityUser?.UserName ?? "unknown";
        //    //            entity.CreatedDate = now;
        //    //        }

        //    //        entity.UpdatedBy = identityUser?.UserName ?? "unknown";
        //    //        entity.UpdatedDate = now;
        //    //    }
        //    //}

        //    return base.SaveChanges();
        //}

    }
}
