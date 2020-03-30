using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Сomments.Queries.GetCommentsList
{
    public class CommentLookUpDto : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasChildComments { get; set; }
        public ICollection<CommentLookUpDto> ChildComments { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentLookUpDto>()
                .ForMember(c => c.Content, c => c.Condition(src => src.IsDeleted != true));
        }
    }
}