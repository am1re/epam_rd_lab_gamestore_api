using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Developers.Queries.GetDevelopersList
{
    public class GetDevelopersListQuery : IRequest<DevelopersListVm>
    {
        public int? GameId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
        
        public class GetDevelopersListQueryHandler : IRequestHandler<GetDevelopersListQuery, DevelopersListVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetDevelopersListQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DevelopersListVm> Handle(GetDevelopersListQuery request, CancellationToken token)
            {
                var query = _context.Developers.AsQueryable();
                
                query = (request.GameId.HasValue)
                    ? query.Where(developer => developer.Games.Any(game => game.Id == request.GameId))
                    : query;
                
                var total = query.Count();
                query = query.Skip(request.Offset).Take(request.Limit);
                
                var developers = await query.ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider).ToListAsync(token);
                
                return new DevelopersListVm
                {
                    Pagination = new PaginationInfo
                    {
                        Limit = request.Limit,
                        Offset = request.Offset,
                        Count = developers.Count,
                        Total = total
                    },
                    Data = developers
                };
            }
        }
    }
}
