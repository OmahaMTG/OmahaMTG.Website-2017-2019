using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmahaMtg.Data;

namespace OmahaMtg.Events
{
    public class EventManager : IEventManager
    {
        private ApplicationDbContext _context;
        public EventManager()
        {
            _context = new ApplicationDbContext();
            AvailableGroups = _context.Groups.ToDictionary(g => g.Id, g => g.Name); 
        }

        public static Dictionary<int, string> AvailableGroups;


        public PagedSet<EventInfo> GetEvents(int skip, int take, bool includeExpired, bool includeRsvpCount, bool includeDeleted)
        {
            var posts = _context.Events
                .Where(w => w.PublishStartTime <= DateTime.Now || w.PublishStartTime == null || includeExpired)
                .Where(w => w.PublishEndTime >= DateTime.Now || w.PublishEndTime == null || includeExpired)
                .Where(w => includeDeleted || !w.IsDeleted)
                .OrderByDescending(o => o.PublishStartTime)
                .Include(cbu => cbu.CreatedByUser)
                .Include(g => g.Groups)
                
                .Select(s => s);

            int totalPosts = posts.Count();

            var md = new MarkdownDeep.Markdown();
            md.ExtraMode = true;
            md.SafeMode = false;

            var mappedPosts = posts.Skip(skip).Take(take).ToList()
                .Select(s =>
                   new EventInfo()
                    {
                        PublishStartTime = s.PublishStartTime,
                        Body = s.Body,
                        HtmlBody = md.Transform(s.Body),
                        HtmlBodySummary = md.Transform(s.Body.Length <= 500 ? s.Body : s.Body.Substring(0, 500).TrimEnd() + "...."),
                        CreatedByUserId = s.CreatedByUserId,
                        CreatedByUserName = GetUserName(s.CreatedByUser),
                        EventEndTime = (s as Event).EventEndTime,
                        EventStartTime = (s as Event).EventStartTime,
                        Id = s.Id,
                        Title = s.Title,
                        GroupTags = s.Groups.Select(g=> g.Id), 
                        PublishEndTime =  s.PublishEndTime,
                        IsDeleted = s.IsDeleted,
                        AvailableGroups = AvailableGroups,
                        VimeoId = (s as Event).VimeoId, 
                        TotalRsvpCount = includeRsvpCount ?  _context.Rsvps.Count(w => w.EventId == s.Id): 0
                        
                    });

            return new PagedSet<EventInfo>(totalPosts,skip, take,  mappedPosts);
        }

        private static string GetUserName(User user)
        {
            var userName = string.Empty;

            if (user != null)
            {
                userName = user.FirstName + " " + user.LastName;
                userName = string.IsNullOrWhiteSpace(userName) ? user.UserName : userName;
            }
            return userName;
        }

        public EventInfo GetEvent(int postId)
        {
            var post = _context.Events
                .Include(g => g.Groups)
                .Include(u => u.CreatedByUser)
                .FirstOrDefault(w => w.Id == postId);

            var md = new MarkdownDeep.Markdown();
            md.ExtraMode = true;
            md.SafeMode = false;

            EventInfo mappedPost;



                mappedPost = new EventInfo()
                {
                    PublishStartTime = post.PublishStartTime,
                    PublishEndTime = post.PublishEndTime,
                    IsDeleted = post.IsDeleted,
                    Body = post.Body,
                    HtmlBody = md.Transform(post.Body),
                    HtmlBodySummary = md.Transform(post.Body.Length <= 500 ? post.Body : post.Body.Substring(0, 500)),
                    CreatedByUserId = post.CreatedByUserId,
                    EventEndTime = (post as Event).EventEndTime,
                    EventStartTime = (post as Event).EventStartTime,
                    Id = post.Id,
                    Title = post.Title,
                    AvailableGroups = AvailableGroups,
                    GroupTags = post.Groups.Select(g => g.Id),
                    CreatedByUserName = GetUserName(post.CreatedByUser),
                    VimeoId = (post as Event).VimeoId
                };

            return mappedPost;
        }

        public EventInfo GetEvent(int postId, Guid userId)
        {
            var result = GetEvent(postId);


            var totalRsvp = _context.Rsvps.Where(w => w.EventId == postId).Count();

            var isUserRsvp = _context.Rsvps.Where(w => w.EventId == postId && w.UserId == userId).Count() > 0;

            ((EventInfo)result).IsUserRsvpd = isUserRsvp;
            ((EventInfo)result).TotalRsvpCount = totalRsvp;


            return result;

        }

        public RsvpInfo UpdateRsvp(Guid userId, int eventId, bool isUserGoing)
        {
            var currentRSVP = _context.Rsvps.Where(w => w.UserId == userId && w.EventId == eventId).FirstOrDefault();
            if(isUserGoing)
            {
                if(currentRSVP == null)   
                {
                    _context.Rsvps.Add(new Rsvp(){
                        UserId = userId,
                        EventId = eventId,
                        RsvpTime = DateTime.Now
                    });
                }
            }else{
                if(currentRSVP != null)
                    _context.Rsvps.Remove(currentRSVP);
            }

            _context.SaveChanges();

            return new RsvpInfo(){
                IsUserGoing = isUserGoing,
                TotalUsersAttending = _context.Rsvps.Where(w => w.EventId == eventId).Count()
            };

            
        }

        private IEnumerable<EventInfo> MapEventToEventInfo(IEnumerable<Event> sourceEvent)
        {
            var md = new MarkdownDeep.Markdown {ExtraMode = true, SafeMode = false};

            return sourceEvent.Select(s => new EventInfo()
            {
                PublishStartTime = s.PublishStartTime,
                Body = s.Body,
                HtmlBody = md.Transform(s.Body),
                HtmlBodySummary = md.Transform(s.Body.Length <= 500 ? s.Body : s.Body.Substring(0, 500)),
                CreatedByUserId = s.CreatedByUserId,
                EventEndTime = (s as Event).EventEndTime,
                EventStartTime = (s as Event).EventStartTime,
                Id = s.Id,
                Title = s.Title,
                AvailableGroups = AvailableGroups
            });
        }

        public IEnumerable<EventInfo> GetUpcomingEvents(int numberToGet)
        {
            var events = _context.Events.Where(w => w.EventEndTime > DateTime.Now).OrderBy(o => o.EventStartTime).Take(numberToGet);
            return MapEventToEventInfo(events);
        }

        public IEnumerable<EventInfo> GetLatestEventsWithVideos(int numberToGet)
        {
            var events = _context.Events.Where(w => w.VimeoId > 0).OrderBy(o => o.EventStartTime).Take(numberToGet);
            return MapEventToEventInfo(events);
        }

        public void UpdateEvent(EventInfo updateEvent)
        {
            var eventToUpdate = _context.Events
                 .Include(cbu => cbu.CreatedByUser)
                .Include(g => g.Groups)
                .FirstOrDefault(w => w.Id == updateEvent.Id);

            eventToUpdate.Groups = _context.Groups.Where(g => updateEvent.GroupTags.Contains(g.Id)).ToList();
            eventToUpdate.IsDeleted = updateEvent.IsDeleted;
            eventToUpdate.EventEndTime = updateEvent.EventEndTime;
            eventToUpdate.EventStartTime = updateEvent.EventStartTime;
            eventToUpdate.PublishEndTime = updateEvent.PublishEndTime;
            eventToUpdate.PublishStartTime = updateEvent.PublishStartTime;
            eventToUpdate.Title = updateEvent.Title;
            eventToUpdate.Body = updateEvent.Body;
            eventToUpdate.VimeoId = updateEvent.VimeoId;

            _context.SaveChanges();
        }

        public int CreateEvent(EventInfo newEvent)
        {
            var newDbEvent = new Event();

            newDbEvent.Groups = _context.Groups.Where(g => newEvent.GroupTags.Contains(g.Id)).ToList();
            newDbEvent.IsDeleted = newEvent.IsDeleted;
            newDbEvent.EventEndTime = newEvent.EventEndTime;
            newDbEvent.EventStartTime = newEvent.EventStartTime;
            newDbEvent.PublishEndTime = newEvent.PublishEndTime;
            newDbEvent.PublishStartTime = newEvent.PublishStartTime;
            newDbEvent.Title = newEvent.Title;
            newDbEvent.Body = newEvent.Body;
            newDbEvent.CreatedByUserId = newEvent.CreatedByUserId;
            newDbEvent.VimeoId = newEvent.VimeoId;
            _context.Events.Add(newDbEvent);
            _context.SaveChanges();

            return newDbEvent.Id;
        }

        public Dictionary<int, string> GetAvailableGroups()
        {
            return AvailableGroups;
        }

    }
}
