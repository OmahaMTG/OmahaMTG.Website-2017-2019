using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Post
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
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
                var newRecord = request.ToPostData();
                _dbContext.Posts.Add(newRecord);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newRecord.ToPost();
            }
        }
    }
}