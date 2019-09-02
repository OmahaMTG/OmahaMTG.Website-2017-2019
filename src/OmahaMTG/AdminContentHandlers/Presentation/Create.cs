using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presentation
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
            
            public string Title { get; set; }
            public string Details { get; set; }
            public IEnumerable<int> PresenterIds { get; set; }
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
                var newRecord = request.ToPresentationData();
                _dbContext.Presentations.Add(newRecord);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newRecord.ToPresentation();
            }
        }
    }
}