using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Meeting
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
                var meetingFromDatabase = await _dbContext.Meetings.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (meetingFromDatabase != null)
                {
                    if (request.Perm)
                    {

                        _dbContext.Meetings.Remove(meetingFromDatabase);
                    }
                    else
                    {
                        meetingFromDatabase.IsDeleted = true;
                    }
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return default;
            }
        }
    }
}