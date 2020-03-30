using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Category
    {
        public Category()
        {
            ChildCategories = new Collection<Category>();
            GameCategories = new Collection<GameCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}