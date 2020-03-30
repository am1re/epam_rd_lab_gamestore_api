using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public CreateDeveloperCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
            {
                var entity = new Developer();
                
                _context.Developers.Add(entity);
                
                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
