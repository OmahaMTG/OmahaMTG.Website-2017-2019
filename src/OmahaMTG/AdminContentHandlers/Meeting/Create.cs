using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;
using System.Linq;

namespace OmahaMTG.AdminContentHandlers.Meeting
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public DateTime? PublishStartTime { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public bool IsDraft { get; set; }
            public int? HostId { get; set; }
            public IEnumerable<int> SponsorIds { get; set; }
            public IEnumerable<int> PresentationIds { get; set; }
            public IEnumerable<string> Tags { get; set; }
            public long? VimeoId { get; set; }
        }

        class CommandHandler : IRequestHandler<Command, Model>
        {
            private readonly UserGroupContext _dbContext;
            public CommandHandler(UserGroupContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Model> Handle(Command request, CancellationToken cancellationToken)
            {
                var newMeeting = request.ToMeetingData();
                await _dbContext.Meetings.AddAsync(newMeeting, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (await _dbContext.Meetings
                   .Include(i => i.MeetingSponsors).ThenInclude(i => i.Sponsor)
                   .Include(i => i.MeetingHost)
                   .Include(i => i.Presentations).ThenInclude(i => i.PresentationPresenters).ThenInclude(i => i.Presenter)
                   .Include(_ => _.MeetingTags).ThenInclude(_ => _.Tag)
                   .Where(w => w.Id == newMeeting.Id)
                   .FirstOrDefaultAsync(cancellationToken: cancellationToken)).ToMeeting();

            }
        }
    }
}