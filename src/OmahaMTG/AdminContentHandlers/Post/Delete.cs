using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Post
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
                var postToDelete = await _dbContext.Posts.FirstOrDefaultAsync(w => w.Id == request.Id);

                if (postToDelete != null)
                {
                    if (request.Perm)
                    {
                        _dbContext.Posts.Remove(postToDelete);
                    }
                    else
                    {
                        postToDelete.IsDeleted = true;
                    }
                    await _dbContext.SaveChangesAsync();

                }

                return default;
            }
        }
    }
}