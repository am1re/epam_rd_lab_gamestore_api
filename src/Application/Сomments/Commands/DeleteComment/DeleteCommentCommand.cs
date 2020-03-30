using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Сomments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCommentCommand>
        {
            private readonly IGameStoreDbContext _context;

            public DeleteCategoryCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Comments.FindAsync(request.Id);

                if (entity == null)
                { 
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                _context.Comments.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
