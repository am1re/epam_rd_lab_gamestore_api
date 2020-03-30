using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Developers.Commands.UpdateDeveloper
{
    public class UpdateDeveloperCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public UpdateDeveloperCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Developers.FindAsync(request.Id);

                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
