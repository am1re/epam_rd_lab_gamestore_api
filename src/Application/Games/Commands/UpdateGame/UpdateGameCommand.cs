using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Games.Commands.UpdateGame
{
    public class UpdateGameCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PublisherId { get; set; }
        public int? DeveloperId { get; set; }

        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public UpdateGameCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Games.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Game), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.Description = request.Description ?? entity.Description;
                entity.Price = request.Price ?? entity.Price;
                entity.IsDeleted = request.IsDeleted ?? entity.IsDeleted;
                entity.PublisherId = request.PublisherId ?? entity.PublisherId;
                entity.DeveloperId = request.DeveloperId ?? entity.DeveloperId;

                await _context.SaveChangesAsync(cancellationToken);
                
                return entity.Id;
            }
        }
    }
}
