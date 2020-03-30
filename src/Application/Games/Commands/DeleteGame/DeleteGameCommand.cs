using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.Games.Commands.DeleteGame
{
    public class DeleteGameCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
        {
            private readonly IGameStoreDbContext _context;

            public DeleteGameCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Games.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Game), request.Id);
                }

                _context.Games.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
