using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Publishers.Queries.GetPublishersList
{
    public class GetPublishersListQuery : IRequest<PublishersListVm>
    {
        public int? GameId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
        
        public class GetPublishersListQueryHandler : IRequestHandler<GetPublishersListQuery, PublishersListVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetPublishersListQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PublishersListVm> Handle(GetPublishersListQuery request, CancellationToken token)
            {
                var query = _context.Publishers.AsQueryable();
                
                query = (request.GameId.HasValue)
                    ? query.Where(publisher => publisher.Games.Any(game => game.Id == request.GameId))
                    : query;
                
                var total = query.Count();
                query = query.Skip(request.Offset).Take(request.Limit);
                
                var publishers = await query.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(token);
                
                return new PublishersListVm
                {
                    Pagination = new PaginationInfo
                    {
                        Limit = request.Limit,
                        Offset = request.Offset,
                        Count = publishers.Count,
                        Total = total
                    },
                    Data = publishers
                };
            }
        }
    }
}
