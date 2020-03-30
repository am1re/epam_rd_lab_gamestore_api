using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Developers.Queries
{
    public class DeveloperDto : IMapFrom<Developer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
