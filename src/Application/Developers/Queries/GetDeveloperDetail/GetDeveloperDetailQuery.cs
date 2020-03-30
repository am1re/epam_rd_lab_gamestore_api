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

namespace Application.Developers.Queries.GetDeveloperDetail
{
    public class GetDeveloperDetailQuery : IRequest<DeveloperDetailVm>
    {
        public int Id { get; set; }

        public class GetDeveloperDetailQueryHandler : IRequestHandler<GetDeveloperDetailQuery, DeveloperDetailVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetDeveloperDetailQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DeveloperDetailVm> Handle(GetDeveloperDetailQuery request, CancellationToken cancellationToken)
            {
                var res = await _context.Developers
                    .Where(e => e.Id == request.Id)
                    .ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
                
                return new DeveloperDetailVm
                {
                    Data = res ?? throw new NotFoundException(nameof(Developer), request.Id)
                };
            }
        }
    }
}
