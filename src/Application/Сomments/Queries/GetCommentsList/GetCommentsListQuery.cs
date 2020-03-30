using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Сomments.Queries.GetCommentsList
{
    public class GetCommentsListQuery : IRequest<CommentsListVm>
    {
        public int? GameId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
        public int Depth { get; set; } = 0;

        public class GetDevelopersListQueryHandler : IRequestHandler<GetCommentsListQuery, CommentsListVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetDevelopersListQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommentsListVm> Handle(GetCommentsListQuery request, CancellationToken token)
            {
                var query = _context.Comments.Where(x => x.ParentCommentId == null);

                query = (request.GameId.HasValue)
                    ? query.Where(comment => comment.GameId == request.GameId)
                    : query;

                var total = query.Count();
                query = query.Skip(request.Offset).Take(request.Limit);

                var includeQuery = (request.Depth > 0)
                    ? query.Include(e => e.ChildComments)
                    : null;

                if (includeQuery != null)
                {
                    for (var i = 0; i < request.Depth - 1; i++)
                        includeQuery = includeQuery.ThenInclude(e => e.ChildComments);

                    query = includeQuery;
                }

                var comments = _mapper.Map<List<Comment>, List<CommentLookUpDto>>(
                    await query.ToListAsync(token),
                    opts => opts.AfterMap((list, dtos) =>
                        CalculateHasChildComments(dtos.AsEnumerable()))
                );

                var res = new CommentsListVm
                {
                    Pagination = new PaginationInfo
                    {
                        Limit = request.Limit,
                        Offset = request.Offset,
                        Count = comments.Count,
                        Total = total,
                    },
                    Depth = request.Depth,
                    Data = comments
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