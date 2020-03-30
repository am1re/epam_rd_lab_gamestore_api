using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Publishers.Commands.CreatePublisher
{
    public class CreatePublisherCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public CreatePublisherCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
            {
                var entity = new Publisher();
                
                _context.Publishers.Add(entity);
                
                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
