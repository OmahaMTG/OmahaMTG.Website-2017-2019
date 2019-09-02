using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Post
{
    public class Update
    {
        public class Command : IRequest<Model>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public DateTime? PublishStartTime { get; set; }
            public bool IsDraft { get; set; }
            public IEnumerable<string> Tags { get; set; }
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
                var postToUpdate = await _dbContext.Posts.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (postToUpdate != null)
                {
                    postToUpdate.ApplyUpdatePostRequestToPostData(request);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return postToUpdate.ToPost();
            }
        }
    }
}