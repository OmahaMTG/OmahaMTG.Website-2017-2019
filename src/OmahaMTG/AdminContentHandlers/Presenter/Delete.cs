using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presenter
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public bool Perm { get; set; }
        }

        class CommandHandler : IRequestHandler<Command>
        {
            private readonly UserGroupContext _dbContext;
            public CommandHandler(UserGroupContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var presenterToDelete = await _dbContext.Presenters.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (presenterToDelete != null)
                {
                    if (request.Perm)
                    {
                        _dbContext.Presenters.Remove(presenterToDelete);
                    }
                    else
                    {
                        presenterToDelete.IsDeleted = true; ;
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return default;
            }
        }
    }
}