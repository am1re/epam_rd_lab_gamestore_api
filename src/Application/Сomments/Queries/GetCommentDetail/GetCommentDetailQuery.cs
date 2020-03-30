using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Сomments.Queries.GetCommentsList;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Сomments.Queries.GetCommentDetail
{
    public class GetCommentDetailQuery : IRequest<CommentDetailVm>
    {
        public int Id { get; set; }
        
        public int Depth { get; set; } = 0;

        public class GetDeveloperDetailQueryHandler : IRequestHandler<GetCommentDetailQuery, CommentDetailVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetDeveloperDetailQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommentDetailVm> Handle(GetCommentDetailQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Comments
                    .Where(e => e.Id == request.Id);
                
                if(!query.Any()) throw new NotFoundException(nameof(Comment), request.Id);
                
                var includeQuery = (request.Depth > 0)
                    ? query.Include(e => e.ChildComments)
                    : null;

                if (includeQuery != null)
                {
                    for (var i = 0; i < request.Depth - 1; i++)
                        includeQuery = includeQuery.ThenInclude(e => e.ChildComments);

                    query = includeQuery;
                }

                var comment = _mapper.Map<Comment, CommentDetailDto>(
                    await query.SingleOrDefaultAsync(cancellationToken),
                    opts => opts.AfterMap((list, dto) =>
                    {
                        var hasChild = dto.ChildComments != null && dto.ChildComments.Any() ||
                                       _context.Comments.Any(c => c.ParentCommentId == dto.Id);
                        
                        dto.HasChildComments = hasChild;
                        
                        CalculateHasChildComments(dto.ChildComments);
                    })
                );
                
                var res = new CommentDetailVm
                {
                    Depth = request.Depth,
                    Data = comment
                };
                return res;
            }
            
            private void CalculateHasChildComments(IEnumerable<CommentLookUpDto> comments)
            {
                foreach (var dto in comments)
                {
                    var hasChild = dto.ChildComments != null && dto.ChildComments.Any() ||
                                   _context.Comments.Any(comment => comment.ParentCommentId == dto.Id);

                    dto.HasChildComments = hasChild;

                    CalculateHasChildComments(dto.ChildComments);
                }
            }
        }
    }
}
