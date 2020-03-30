using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
        {
            private readonly IGameStoreDbContext _context;

            public DeleteCategoryCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories
                    .Include(e => e.ChildCategories)
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                DeleteCategoryChildren(entity);
                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            private void DeleteCategoryChildren(Category entity)
            {
                var children = entity.ChildCategories;
                _context.Categories.RemoveRange(children);
            }
        }
    }
}
