using System.Linq;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.ViewModels;

namespace Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<CategoriesListVm>
    {
        public int? GameId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;

        public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
        {
            private readonly IGameStoreDbContext _context;
            private readonly IMapper _mapper;

            public GetCategoriesListQueryHandler(IGameStoreDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoriesListVm> Handle(GetCategoriesListQuery request,
                CancellationToken cancellationToken)
            {
                var query = _context.Categories.AsQueryable();

                query = (request.GameId.HasValue)
                    ? query.Where(category => category.GameCategories.Any(c => c.GameId == request.GameId))
                    : query;

                var total = query.Count();
                query = query.Skip(request.Offset).Take(request.Limit);
                
                var categories = await query.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new CategoriesListVm
                {
                    Pagination = new PaginationInfo
                    {
                        Limit = request.Limit,
                        Offset = request.Offset,
                        Count = categories.Count,
                        Total = total
                    },
                    Data = categories
                };
            }
        }
    }
}