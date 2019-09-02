using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;
using System.Linq;
namespace OmahaMTG.AdminContentHandlers.Presentation
{
    public class Query
    {
        public class Command : SkipTakeRequest, IRequest<SkipTakeSet<Model>>
        {
            public string Filter { get; set; }
            public int? MeetingId { get; set; }
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
                var result = (await _dbContext.Presentations
                    .Include(_ => _.PresentationPresenters)
                    .Where(_ => request.IncludeDeleted || !_.IsDeleted)
                    .Where(_ => string.IsNullOrWhiteSpace(request.Filter) || EF.Functions.Like(_.Title, $"%{request.Filter}%") || EF.Functions.Like(_.Details, $"%{request.Filter}%"))
                    .Where(_ => !request.MeetingId.HasValue || _.MeetingId == request.MeetingId.Value)
                    .OrderBy(_ => _.CreatedDate)
                    .AsSkipTakeSet(request.Skip, request.Take, d => d.ToPresentation()));

                return result;
            }
        }
    }
}