using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presenter
{
    public class Update
    {
        public class Command : IRequest<Model>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public string OmahaMtgUserId { get; set; }
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
                var presenterToUpdate = await _dbContext.Presenters.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (presenterToUpdate != null)
                {
                    presenterToUpdate.ApplyUpdatePresenterRequestToPresenterData(request);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return presenterToUpdate.ToPresenter();
            }
        }
    }
}