using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public CreateCategoryCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category();
                
                _context.Categories.Add(entity);

                entity.Name = request.Name;
                entity.ParentCategoryId = request.ParentCategoryId ?? entity.ParentCategoryId; // TODO: check if category exists

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}