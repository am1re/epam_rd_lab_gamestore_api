using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasOne(e => e.ParentCategory)
                .WithMany(e => e.ChildCategories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
