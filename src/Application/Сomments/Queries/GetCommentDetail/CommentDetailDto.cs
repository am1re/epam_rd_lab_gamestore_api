using System.Collections.Generic;
using Application.Common.Mappings;
using Application.Сomments.Queries.GetCommentsList;
using AutoMapper;
using Domain.Entities;

namespace Application.Сomments.Queries.GetCommentDetail
{
    public class CommentDetailDto : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentCommentId { get; set; }
        public bool HasChildComments { get; set; }
        public ICollection<CommentLookUpDto> ChildComments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDetailDto>()
                .ForMember(c => c.Content, c => c.Condition(src => src.IsDeleted != true));
        }
    }
}
