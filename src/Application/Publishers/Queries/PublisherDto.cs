using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Publishers.Queries
{
    public class PublisherDto : IMapFrom<Publisher>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
