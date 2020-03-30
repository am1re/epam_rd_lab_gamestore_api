using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Сomments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool? IsDeleted { get; set; }
        
        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public UpdateCommentCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Comments.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }
                
                entity.Content = request.Content ?? entity.Content;
                entity.IsDeleted = request.IsDeleted ?? entity.IsDeleted;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
