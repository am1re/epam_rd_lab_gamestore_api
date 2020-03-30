using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.Games.Queries.GetGameDetail
{
    public class GetGameDetailQuery : IRequest<GameDetailVm>
    {
        public int Id { get; set; }
        
        public class GetGameDetailQueryHandler : IRequestHandler<GetGameDetailQuery, GameDetailVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetGameDetailQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GameDetailVm> Handle(GetGameDetailQuery request, CancellationToken cancellationToken)
            {
                var res = await _context.Games
                    .Where(e => e.Id == request.Id && e.IsDeleted != true)
                    .ProjectTo<GameDetailDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
                
                return new GameDetailVm
                {
                    Data = res ?? throw new NotFoundException(nameof(Game), request.Id)
                };
            }
        }
    }
}
