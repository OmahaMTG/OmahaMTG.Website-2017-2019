using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Posts
{
    public interface IPostManager
    {
        PagedSet<PostInfo> GetPosts(int skip, int take, bool includeExpired);
        PostInfo GetPost(int postId, Guid userId);
        PostInfo GetPost(int postId);
        RsvpInfo UpdateRsvp(Guid userId, int eventId, bool isUserGoing);

        void UpdateEvent(EventInfo updateEvent);
        int CreateEvent(EventInfo newEvent);

        IEnumerable<EventInfo> GetLatestEventsWithVideos(int numberToGet);
        IEnumerable<EventInfo> GetUpcomingEvents(int numberToGet);
         Dictionary<int, string> GetAvailableGroups();
    }
}
