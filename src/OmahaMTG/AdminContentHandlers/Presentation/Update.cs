using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presentation
{
    public class Update
    {
        public class Command : IRequest<Model>
        {
            public int Id { get; set; }
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
                var presentationToUpdate = await _dbContext.Presentations.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (presentationToUpdate != null)
                {
                    presentationToUpdate.ApplyUpdatePresentationRequestToPresentationData(request);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return presentationToUpdate.ToPresentation();
            }
        }
    }
}