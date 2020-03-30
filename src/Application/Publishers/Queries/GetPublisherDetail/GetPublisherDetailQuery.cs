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

namespace Application.Publishers.Queries.GetPublisherDetail
{
    public class GetPublisherDetailQuery : IRequest<PublisherDetailVm>
    {
        public int Id { get; set; }

        public class GetPublisherDetailQueryHandler : IRequestHandler<GetPublisherDetailQuery, PublisherDetailVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetPublisherDetailQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PublisherDetailVm> Handle(GetPublisherDetailQuery request, CancellationToken cancellationToken)
            {
                var res = await _context.Publishers
                    .Where(e => e.Id == request.Id)
                    .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
                
                return new PublisherDetailVm
                {
                    Data = res ?? throw new NotFoundException(nameof(Publisher), request.Id)
                };
            }
        }
    }
}
