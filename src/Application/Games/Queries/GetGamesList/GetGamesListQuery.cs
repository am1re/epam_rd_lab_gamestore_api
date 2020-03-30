using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Queries.GetGamesList
{
    public class GetGamesListQuery : IRequest<GamesListVm>
    {
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public int? DeveloperId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;

        public class GetGamesListQueryHandler : IRequestHandler<GetGamesListQuery, GamesListVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetGamesListQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GamesListVm> Handle(GetGamesListQuery request, CancellationToken token)
            {
                var query = _context.Games.Where(g => g.IsDeleted != true);
                
                query = (request.CategoryId.HasValue)
                    ? query.Where(game => game.GameCategories.Any(c => c.CategoryId == request.CategoryId))
                    : query;
                query = (request.PublisherId.HasValue)
                    ? query.Where(game => game.PublisherId == request.PublisherId)
                    : query;
                query = (request.DeveloperId.HasValue)
                    ? query.Where(game => game.DeveloperId == request.DeveloperId)
                    : query;

                var total = query.Count();
                query = query.Skip(request.Offset).Take(request.Limit);
                
                var games = await query.ProjectTo<GameLookUpDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(token);
                
                return new GamesListVm
                {
                    Pagination = new PaginationInfo
                    {
                        Limit = request.Limit,
                        Offset = request.Offset,
                        Count = games.Count,
                        Total = total
                    },
                    Data = games
                };
            }
        }
    }
}