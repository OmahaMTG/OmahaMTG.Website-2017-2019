using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmahaMTG.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OmahaMTG.AdminContentHandlers.Host
{
    public class Query
    {
        public class Command : SkipTakeRequest, IRequest<SkipTakeSet<Model>>
        {
            public String Filter { get; set; }
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
                var result = (await _dbContext.Hosts
                    .Where(p => request.IncludeDeleted || !p.IsDeleted)
                    .Where(p => string.IsNullOrWhiteSpace(request.Filter) || EF.Functions.Like(p.Name, $"%{request.Filter}%"))
                    .OrderBy(p => p.Name)
                    .ThenBy(p => p.CreatedDate)
                    .AsSkipTakeSet(request.Skip, request.Take, d => d.ToHost()));

                return result;
            }
        }
    }
}