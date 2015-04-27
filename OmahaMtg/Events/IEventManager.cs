using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Events
{
    public interface IEventManager
    {
        PagedSet<EventInfo> GetEvents(int skip, int take, bool includeExpired, bool includeRsvpCount, bool includeDeleted);

        EventInfo GetEvent(int eventId, Guid userId);
        EventInfo GetEvent(int eventId);
        RsvpInfo UpdateRsvp(Guid userId, int eventId, bool isUserGoing);

        void UpdateEvent(EventInfo updateEvent);
        int CreateEvent(EventInfo newEvent);

        IEnumerable<EventInfo> GetLatestEventsWithVideos(int numberToGet);
        IEnumerable<EventInfo> GetUpcomingEvents(int numberToGet);
         Dictionary<int, string> GetAvailableGroups();

    }
}
