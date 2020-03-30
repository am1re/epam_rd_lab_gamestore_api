using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.FirstName)
                .HasMaxLength(32)
                .IsRequired();
            
            builder.Property(e => e.LastName)
                .HasMaxLength(32)
                .IsRequired();
            
            builder.Property(e => e.Email)
                .HasMaxLength(32)
                .IsRequired();
            
            builder.Property(e => e.Phone)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.PaymentTypeId)
                .IsRequired();

            builder.Property(e => e.OrderStatusId)
                .IsRequired();
        }
    }
}
