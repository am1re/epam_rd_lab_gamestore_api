using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Game : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public Developer Developer { get; set; }
        public int? DeveloperId { get; set; }
        public ICollection<GameCategory> GameCategories { get; set; }
    }
}