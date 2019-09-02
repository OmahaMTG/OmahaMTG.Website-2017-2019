using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Sponsor
{
    public class Update
    {
        public class Command : IRequest<Model>
        {
            public int Id { get; set; }
            public int Type { get; set; }
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
                var sponsorToUpdate = await _dbContext.Sponsors.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (sponsorToUpdate != null)
                {
                    sponsorToUpdate.ApplyUpdateSponsorRequestToSponsorData(request);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return sponsorToUpdate.ToSponsor();
            }
        }
    }
}