using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Sponsor
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
            public string Name { get; set; }
            public string Blurb { get; set; }
            public string ContactInfo { get; set; }
            public string Url { get; set; }
            public string ShortBlurb { get; set; }
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
                var newRecord = request.ToSponsorData();
                _dbContext.Sponsors.Add(newRecord);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newRecord.ToSponsor();
            }
        }
    }
}