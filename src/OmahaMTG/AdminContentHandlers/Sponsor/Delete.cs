using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Sponsor
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
                var sponsorFromDatabase = await _dbContext.Sponsors.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (sponsorFromDatabase != null)
                {
                    if (request.Perm)
                    {
                        _dbContext.Sponsors.Remove(sponsorFromDatabase);
                    }
                    else
                    {
                        sponsorFromDatabase.IsDeleted = true; ;
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return default;
            }
        }
    }
}