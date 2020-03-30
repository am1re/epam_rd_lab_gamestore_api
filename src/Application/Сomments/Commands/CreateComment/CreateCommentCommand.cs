using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Сomments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public int GameId { get; set; }
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
        {
            private readonly IGameStoreDbContext _context;

            public CreateCommentCommandHandler(IGameStoreDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var entity = new Comment()
                {
                    GameId = request.GameId,
                    Content = request.Content,
                    ParentCommentId = request.ParentCommentId
                };

                _context.Comments.Add(entity);

                if (!_context.Games.Any(game => game.Id == entity.GameId))
                    throw new ConflictDataException(nameof(GameId),
                        $"Entity 'Game' ({entity.GameId}) was not found.");

                if (entity.ParentCommentId.HasValue)
                {
                    var parentComment = (_context.Comments.Any(c => c.Id == entity.ParentCommentId))
                        ? await _context.Comments.FindAsync(entity.ParentCommentId)
                        : throw new ConflictDataException(nameof(ParentCommentId),
                            $"Entity 'Comment' ({entity.ParentCommentId}) was not found.");

                    if (parentComment.GameId != entity.GameId)
                        throw new ConflictDataException(nameof(GameId),
                            $"Comment can't have different GameId from his parent. Expected GameId: {parentComment.GameId}");
                }

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}