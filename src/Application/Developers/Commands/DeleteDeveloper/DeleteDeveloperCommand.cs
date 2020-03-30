using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Developers.Commands.DeleteDeveloper
{
    public class DeleteDeveloperCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteDeveloperCommand>
        {
            private readonly IGameStoreDbContext _context;

            public DeleteCategoryCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Developers.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Developer), request.Id);
                }

                _context.Developers.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}