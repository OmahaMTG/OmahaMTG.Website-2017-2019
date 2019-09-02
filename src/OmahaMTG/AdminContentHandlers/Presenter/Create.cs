using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presenter
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
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
                var newRecord = request.ToPresenterData();
                _dbContext.Presenters.Add(newRecord);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newRecord.ToPresenter();
            }
        }
    }
}