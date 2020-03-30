using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Publishers.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public UpdatePublisherCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Publishers.FindAsync(request.Id);

                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
