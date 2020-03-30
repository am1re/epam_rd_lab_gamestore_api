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

namespace Application.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQuery : IRequest<CategoryDetailVm>
    {
        public int Id { get; set; }

        public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetCategoryDetailQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoryDetailVm> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
            {
                var res = await _context.Categories
                    .Where(e => e.Id == request.Id)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
                
                return new CategoryDetailVm
                {
                    Data = res ?? throw new NotFoundException(nameof(Category), request.Id)
                };
            }
        }
    }
}
