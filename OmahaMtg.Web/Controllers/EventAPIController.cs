using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OmahaMtg.Events;

namespace OmahaMtg.Web.Controllers
{
    [Route("api/Event")]
    public class EventAPIController : ApiController
    {
        private OmahaMtg.Events.IEventManager _em;
        public EventAPIController()
        {
            _em = new EventManager();
        }

        // GET: api/EventAPI
        public IEnumerable<EventInfo> Get(bool past = false)
        {
            if (past)
            {
                return _em.GetLatestEventsWithVideos(5);
            }
            else
            {
                return _em.GetUpcomingEvents(5);
            }

        }


    }
}
