using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Meeting
{
    public class Query
    {
        public class Command : SkipTakeRequest, IRequest<SkipTakeSet<Model>>
        {
            public String Filter { get; set; }

            public bool IncludeDrafts { get; set; }
            public bool IncludeDeleted { get; set; }
        }

        class CommandHandler : IRequestHandler<Command, SkipTakeSet<Model>>
        {
            private readonly UserGroupContext _dbContext;
            public CommandHandler(UserGroupContext dbContext)
            {
                _dbContext = dbContext;
            }


            public async Task<SkipTakeSet<Model>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await
                    _dbContext.Meetings
                        .Include(i => i.MeetingSponsors).ThenInclude(i => i.Sponsor)
                        .Include(i => i.MeetingHost)
                        .Include(i => i.Presentations).ThenInclude(i => i.PresentationPresenters)
                        .ThenInclude(i => i.Presenter)
                        .Include(_ => _.MeetingTags).ThenInclude(_ => _.Tag)
                        .Where(p => request.IncludeDeleted || !p.IsDeleted)
                        .Where(p => request.IncludeDrafts || !p.IsDraft)
                        .Where(p => string.IsNullOrWhiteSpace(request.Filter) ||
                                    EF.Functions.Like(p.Title, $"%{request.Filter}%"))
                        .AsSkipTakeSet(request.Skip, request.Take, d => d.ToMeeting());

                return result;
            }
        }
    }
}