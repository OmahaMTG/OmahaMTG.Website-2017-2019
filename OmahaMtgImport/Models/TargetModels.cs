using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtgImport.Models
{
    class TargetPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime PublishStartTime { get; set; }
        public DateTime? PublishEndTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    class TargetEvent
    {
        public int Id { get; set; }
        public DateTime? EventStartTime { get; set; }
        public DateTime? EventEndTime { get; set; }
        public string  Location { get; set; }
        public string Sponsor { get; set; }


        [PetaPoco.Ignore]
        public TargetPost TargetPost { get; set; }

        public static void AddEvent(TargetEvent targetEvent)
        {
            var db = new PetaPoco.Database("TargetConnection");
            db.Insert("Posts", "Id", true, targetEvent.TargetPost);

            targetEvent.Id = targetEvent.TargetPost.Id;
            db.Insert("Events", "Id", false, targetEvent);
        }
    }

    class TargetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        private readonly List<string> _admins = new List<string>() { "beolson", "kerdirks", "mruwe", "brammp", "dipetersen" };

        [PetaPoco.Ignore]
        public bool IsAdmin
        {
            get
            {
                if (_admins.Contains(UserName))
                    return true;
                else
                    return false;
            }

        }

        [PetaPoco.Ignore]
        public List<SourceGroup> GroupNames { get; set; } 

        public TargetUser()
        {
            PhoneNumberConfirmed = false;
            EmailConfirmed = true;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;

        }

        public static void AddUser(TargetUser user)
        {
            var db = new PetaPoco.Database("TargetConnection");
            if (!user.IsAdmin)
            {
                user.Email = user.Id.ToString() + "@mailinator.com";
            }
            db.Insert("AspNetUsers", "Id", false, user);

            foreach (var group in user.GroupNames.Where(w => !string.IsNullOrEmpty(w.GroupName)))
            {
                var groupId = TargetGroup.Groups.Where(w => w.Value == group.GroupName).FirstOrDefault().Key;

                db.Insert("UserGroups", "Id", false, new {UserId = user.Id, GroupId = groupId});
            }

            if (user.IsAdmin)
            {
                db.Insert("AspNetUserRoles", "UserId", false,
                    new {UserId = user.Id, RoleId = TargetRoles.Roles.FirstOrDefault().Key});

            }
            
        }
    }


    class TargetGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public static Dictionary<int, string> Groups = new Dictionary<int, string>();


        public static void CreateGroups()
        {
            var db = new PetaPoco.Database("TargetConnection");

            int id;

            db.Insert("Groups", "Id", false, new{Name = ".NET User Group"});
            Groups.Add(1, ".NET User Group");
            db.Insert("Groups", "Id", false, new { Name = "SQL Server/BI User Group" });
            Groups.Add(2, "SQL Server/BI User Group");
            db.Insert("Groups", "Id", false, new { Name = "BizTalk User Group" });
            Groups.Add(3, "BizTalk User Group");
            db.Insert("Groups", "Id", false, new { Name = "Omaha ALM User Group" });
            Groups.Add(4, "Omaha ALM User Group");
            db.Insert("Groups", "Id", false, new { Name = "SharePoint User Group" });
            Groups.Add(5, "SharePoint User Group");

        }
    }

    class TargetRoles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Dictionary<Guid, string> Roles = new Dictionary<Guid, string>();


        public static void CreateRoles()
        {
            var db = new PetaPoco.Database("TargetConnection");

            int id;

            Guid adminGuid = Guid.NewGuid();

            db.Insert("AspNetRoles", "Id", false, new { Name = "Admin", id = adminGuid });
            Roles.Add(adminGuid, "Admin");
            

        }
    }
}
