using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Post
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
                var result = (await _dbContext.Posts
                    .Include(_ => _.PostTags).ThenInclude(_ => _.Tag)
                    .Where(p => request.IncludeDrafts || !p.IsDraft)
                    .Where(p => request.IncludeDeleted || !p.IsDeleted)
                    .Where(p => string.IsNullOrWhiteSpace(request.Filter) || EF.Functions.Like(p.Title, $"%{request.Filter}%") || EF.Functions.Like(p.Body, $"%{request.Filter}%"))
                    //.Where(p => p.PublishStartTime <= DateTime.Now)
                    .OrderBy(p => p.PublishStartTime)
                    .ThenBy(p => p.CreatedDate)
                    .AsSkipTakeSet(request.Skip, request.Take, d => d.ToPost()));

                return result;
            }
        }
    }
}