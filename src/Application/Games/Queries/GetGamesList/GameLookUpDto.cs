using System.Collections.Generic;
using System.Linq;
using Application.Categories.Queries;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Games.Queries.GetGamesList
{
    public class GameLookUpDto : IMapFrom<Game>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Game, GameLookUpDto>()
                .ForMember(dto => dto.Categories, opt => opt.MapFrom(
                    x => x.GameCategories.Select(y => y.Category)))
                .ReverseMap();
        }
    }
}