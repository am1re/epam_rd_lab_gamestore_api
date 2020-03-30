using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
    }
}