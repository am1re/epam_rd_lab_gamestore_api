using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? PublisherId { get; set; }
        public int? DeveloperId { get; set; }

        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public CreateGameCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                var entity = new Game();
                
                _context.Games.Add(entity);

                entity.Name = request.Name;
                entity.Description = request.Description ?? entity.Description;
                entity.Price = request.Price ?? entity.Price;
                entity.PublisherId = request.PublisherId ?? entity.PublisherId;
                entity.DeveloperId = request.DeveloperId ?? entity.DeveloperId;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
