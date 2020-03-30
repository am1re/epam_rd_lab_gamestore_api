using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGameStoreDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Developer> Developers { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<GameCategory> GameCategories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<OrderStatus> OrderStatuses { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
