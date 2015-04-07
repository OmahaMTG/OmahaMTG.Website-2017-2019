using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtgImport.Models
{
    class SourceEvent
    {
        public String Subject { get; set; }
        public String Description { get; set; }
        public string Location { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime PostedOn { get; set; }
        public Guid PostedById { get; set; }
        public int Id { get; set; }

        public static List<SourceEvent> GetEvents()
        {
            var db = new PetaPoco.Database("SourceConnection");
            return db.Query<SourceEvent>(@"Select Subject, Description, Location, StartDate, EndDate, PostedOn, EventStartDate, EventEndDate, username, e.postedbyId, e.id
                                            from [Events] as e
                                            join aspnet_Users as u on e.postedbyId = u.UserId
                                            order by postedon asc").ToList();
        } 
    }

    class SourceGroup
    {
        public string GroupName { get; set; }
    }

    class SourceUser
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public List<SourceGroup> GroupNames { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }




        public static List<SourceUser> GetUsers()
        {
            var db = new PetaPoco.Database("SourceConnection");
            return db.Fetch<SourceUser, SourceGroup, SourceUser>(new UserGroupRelator().MapIt,
                                             @"  select Email, password, passwordSalt, Username, u.UserId, g.Name as GroupName, u2.firstname, u2.lastname
                                            from aspnet_Users as u
                                            left join [dbo].[UserGroupMembership] as ug on u.UserId = ug.userid
                                            left join [dbo].[UserGroups] as g on ug.[UserGroupID] = g.userGroupID
                                            left join aspnet_Membership as m on u.UserId = m.UserId
                                            left join Users as u2 on u.UserId = u2.UserId").ToList();

        } 
    }

    class UserGroupRelator
    {
        public SourceUser current;
        public SourceUser MapIt(SourceUser a, SourceGroup p)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (a == null)
                return current;

            // Is this the same author as the current one we're processing
            if (current != null && current.UserId == a.UserId)
            {
                // Yes, just add this post to the current author's collection of posts
                current.GroupNames.Add(p);

                // Return null to indicate we're not done with this author yet
                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet

            // Save the current author
            var prev = current;

            // Setup the new current author
            current = a;
            current.GroupNames = new List<SourceGroup>();
            current.GroupNames.Add(p);

            // Return the now populated previous author (or null if first time through)
            return prev;
        }
    }

    class SourceEventRSVP
    {
        public Guid UserId { get; set; }
        public int EventId { get; set; }
        public DateTime DateAdded { get; set; }

        public static List<SourceEventRSVP> GetRsvpForEvent(int eventid)
        {
            var db = new PetaPoco.Database("SourceConnection");
            return db.Query<SourceEventRSVP>(@"  SELECT userid , eventid, max(DateAdded)  as dateadded  FROM users_events
                                                    where DateAdded is not null and eventid = @0
                                                    Group by UserId, eventid", eventid).ToList();
        } 
    }

}
