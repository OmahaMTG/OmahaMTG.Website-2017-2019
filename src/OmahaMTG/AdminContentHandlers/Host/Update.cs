using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Host
{
    public class Update
    {
        public class Command : IRequest<Model>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Blurb { get; set; }
            public string Address { get; set; }
            public string ContactInfo { get; set; }
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
                var hostToUpdate = await _dbContext.Hosts.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);
                if (hostToUpdate != null)
                {
                    hostToUpdate.ApplyUpdateHostRequestToHostData(request);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return hostToUpdate.ToHost();
            }
        }
    }
}