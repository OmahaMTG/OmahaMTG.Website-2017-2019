using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Host
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
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
                var newRecord = request.ToHostData();
                _dbContext.Hosts.Add(newRecord);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newRecord.ToHost();
            }
        }
    }
}