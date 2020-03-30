using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(e => e.ParentComment)
                .WithMany(e => e.ChildComments)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.Property(e => e.Content).IsRequired();
            builder.Property(e => e.GameId).IsRequired();
            builder.Property(e => e.CreatedBy).IsRequired();
        }
    }
}
