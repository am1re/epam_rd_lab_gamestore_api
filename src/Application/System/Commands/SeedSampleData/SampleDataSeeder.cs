using Application.Common.Interfaces;
using Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly IGameStoreDbContext _context;

        public SampleDataSeeder(IGameStoreDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            await SeedDevelopersAsync(cancellationToken);
            await SeedPublishersAsync(cancellationToken);
            await SeedCategoriesAsync(cancellationToken);
            await SeedGamesAsync(cancellationToken);
            await SeedCommentsAsync(cancellationToken);
            await SeedOrderStatusesAsync(cancellationToken);
            await SeedPaymentTypesAsync(cancellationToken);
            await SeedOrdersAsync(cancellationToken);
            await SeedOrderDetailsAsync(cancellationToken);
            await SeedGameCategoriessAsync(cancellationToken);   
        }

        private async Task SeedCategoriesAsync(CancellationToken cancellationToken)
        {
            if (_context.Categories.Any()) return;

            var categories = new[]
            {
                new Category {Name = "Strategy"},
                new Category {Name = "RPG"},
                new Category {Name = "Sports"},
                new Category {Name = "Races"}
                .AddCategoryChild(
                    new Category {Name = "Rally"},
                    new Category {Name = "Arcade"},
                    new Category {Name = "Formula"},
                    new Category {Name = "Off-road"}
                ),
                new Category {Name = "Action"}
                .AddCategoryChild(
                    new Category {Name = "FPS" },
                    new Category {Name = "TPS" },
                    new Category {Name = "Misc." }
                ),
                new Category {Name = "Adventure"},
                new Category {Name = "Puzzle & Skill"},
                new Category {Name = "Other"},
            };

            _context.Categories.AddRange(categories);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedCommentsAsync(CancellationToken cancellationToken)
        {
            if (_context.Comments.Any()) return;

            var comments = new[]
            {
                new Comment { Content = "sample comment", GameId = 1, CreatedBy = "noone" },
                new Comment { Content = "sample reply comment", GameId = 1, ParentCommentId = 1, CreatedBy = "noone" },
                new Comment { Content = "sample comment", GameId = 2, CreatedBy = "noone" },
                new Comment { Content = "sample comment", GameId = 3, CreatedBy = "noone" },
                new Comment { Content = "sample comment", GameId = 4, CreatedBy = "noone" },
                new Comment { Content = "sample comment", GameId = 5, CreatedBy = "noone" },
            };

            _context.Comments.AddRange(comments);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedDevelopersAsync(CancellationToken cancellationToken)
        {
            if (_context.Developers.Any()) return;

            var developers = new[]
            {
                new Developer { Name = "Nintendo" },
                new Developer { Name = "Valve Corporation" },
                new Developer { Name = "Rockstar Games" },
                new Developer { Name = "Electronic Arts" },
                new Developer { Name = "Activision Blizzard" },
                new Developer { Name = "Sony Computer Entertainment" },
                new Developer { Name = "Ubisoft" },
                new Developer { Name = "Sega Games Co. Ltd" },
                new Developer { Name = "BioWare" },
                new Developer { Name = "Naughty Dog Inc" },
                new Developer { Name = "Square Enix Holdings Co. Ltd" },
                new Developer { Name = "Capcom Company Ltd" },
                new Developer { Name = "Bungie Inc" },
                new Developer { Name = "Microsoft Corporation" },
                new Developer { Name = "Bandai Namco Entertainment" },
                new Developer { Name = "Mojang" },
                new Developer { Name = "Epic Games" },
                new Developer { Name = "Game Freak" },
                new Developer { Name = "Insomniac Games Inc" },
                new Developer { Name = "Infinity Ward" }
            };

            _context.Developers.AddRange(developers);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedGamesAsync(CancellationToken cancellationToken)
        {
            if (_context.Games.Any()) return;

            var games = new[]
            {
                new Game {Name = "Game 1", Price = 11.00m, PublisherId = 1, DeveloperId = 1, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Game {Name = "Game 2", Price = 12.00m, PublisherId = 2, DeveloperId = 2, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Game {Name = "Game 3", Price = 13.00m, PublisherId = 3, DeveloperId = 3, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Game {Name = "Game 4", Price = 14.00m, PublisherId = 4, DeveloperId = 4, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Game {Name = "Game 5", Price = 15.00m, PublisherId = 5, DeveloperId = 5, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Game {Name = "Game 6", Price = 16.00m, PublisherId = 6, DeveloperId = 6, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            };

            _context.Games.AddRange(games);

            await _context.SaveChangesAsync(cancellationToken);
        }
        private async Task SeedGameCategoriessAsync(CancellationToken cancellationToken)
        {
            if (_context.GameCategories.Any()) return;

            var gamecategories = new[]
            {
                new GameCategory {GameId = 1, CategoryId = 5},
                new GameCategory {GameId = 2, CategoryId = 4},
                new GameCategory {GameId = 3, CategoryId = 3},
                new GameCategory {GameId = 4, CategoryId = 2},
                new GameCategory {GameId = 5, CategoryId = 1},
            };

            _context.GameCategories.AddRange(gamecategories);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedOrdersAsync(CancellationToken cancellationToken)
        {
            if (_context.Orders.Any()) return;

            var orders = new[]
            {
                new Order {FirstName = "Jarred", LastName = "Sambrano", Email = "arachne@yahoo.ca", Phone = "(548) 707 - 2953", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 1, OrderStatusId = 1 },
                new Order {FirstName = "Winston", LastName = "Scalia", Email = "nanop@comcast.net", Phone = "(714) 423 - 6546", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 2, OrderStatusId = 2 },
                new Order {FirstName = "Kina", LastName = "Koepke", Email = "andale@hotmail.com", Phone = "(864) 786 - 1088", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 2, OrderStatusId = 4 },
                new Order {FirstName = "Tona", LastName = "Dammann", Email = "daveed@msn.com", Phone = "(201) 379 - 1594", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 2, OrderStatusId = 3 },
                new Order {FirstName = "Bruna", LastName = "Crumpton", Email = "jshearer@optonline.net", Phone = "(877) 882 - 8380", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 1, OrderStatusId = 3 },
                new Order {FirstName = "Oscar", LastName = "Mends", Email = "dsugal@msn.com", Phone = "(896) 664 - 0196", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 1, OrderStatusId = 1 },
                new Order {FirstName = "Ivan", LastName = "Ptak", Email = "kawasaki@aol.com", Phone = "(995) 943 - 7883", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 2, OrderStatusId = 1 },
                new Order {FirstName = "Felton", LastName = "Bertram", Email = "andrewik@yahoo.ca", Phone = "(839) 814 - 5245", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 1, OrderStatusId = 2 },
                new Order {FirstName = "Shondra", LastName = "Wickwire", Email = "michiel@att.net", Phone = "(473) 501-6216", Comment = "Aliquam eleifend mi in nulla posuere sollicitudin aliquam.", PaymentTypeId = 1, OrderStatusId = 2 }
            };

            _context.Orders.AddRange(orders);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedOrderDetailsAsync(CancellationToken cancellationToken)
        {
            if (_context.OrderDetails.Any()) return;

            var orderdetails = new[]
            {
                new OrderDetail {OrderId = 1, GameId = 1},
                new OrderDetail {OrderId = 1, GameId = 2},
                new OrderDetail {OrderId = 2, GameId = 6},
                new OrderDetail {OrderId = 2, GameId = 4},
                new OrderDetail {OrderId = 3, GameId = 5},
                new OrderDetail {OrderId = 3, GameId = 4},
                new OrderDetail {OrderId = 3, GameId = 3},
                new OrderDetail {OrderId = 4, GameId = 1},
                new OrderDetail {OrderId = 5, GameId = 2},
                new OrderDetail {OrderId = 6, GameId = 2},
                new OrderDetail {OrderId = 6, GameId = 4},
                new OrderDetail {OrderId = 7, GameId = 5},
                new OrderDetail {OrderId = 8, GameId = 5},
                new OrderDetail {OrderId = 9, GameId = 3},
            };

            _context.OrderDetails.AddRange(orderdetails);

            await _context.SaveChangesAsync(cancellationToken);
        }
        private async Task SeedOrderStatusesAsync(CancellationToken cancellationToken)
        {
            if (_context.OrderStatuses.Any()) return;

            var statuses = new[]
            {
                new OrderStatus {Slug = "new"},
                new OrderStatus {Slug = "confirmed"},
                new OrderStatus {Slug = "rejected"},
                new OrderStatus {Slug = "closed"}
            };

            _context.OrderStatuses.AddRange(statuses);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedPaymentTypesAsync(CancellationToken cancellationToken)
        {
            if (_context.PaymentTypes.Any()) return;

            var types = new[]
            {
                new PaymentType {Slug = "cash"},
                new PaymentType {Slug = "card"}
            };

            _context.PaymentTypes.AddRange(types);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedPublishersAsync(CancellationToken cancellationToken)
        {
            if (_context.Publishers.Any()) return;

            var publishers = new[]
            {
                new Publisher{ Name = "Capcom" },
                new Publisher{ Name = "Sega" },
                new Publisher{ Name = "Electronic Arts" },
                new Publisher{ Name = "Nintendo" },
                new Publisher{ Name = "Ubisoft" },
                new Publisher{ Name = "Sony" },
                new Publisher{ Name = "Square Enix" },
                new Publisher{ Name = "Bandai Namco Games" },
                new Publisher{ Name = "Digerati Distribution" },
                new Publisher{ Name = "NIS America" },
                new Publisher{ Name = "Plug In Digital" },
                new Publisher{ Name = "Focus Home Interactive" },
                new Publisher{ Name = "THQ Nordic" },
                new Publisher{ Name = "Activision Blizzard" },
                new Publisher{ Name = "Paradox Interactive" },
                new Publisher{ Name = "505 Games" },
                new Publisher{ Name = "Take-Two Interactive" },
                new Publisher{ Name = "Aksys Games" },
                new Publisher{ Name = "Bethesda Softworks" },
                new Publisher{ Name = "Microsoft" },
                new Publisher{ Name = "Devolver Digital" }
            };

            _context.Publishers.AddRange(publishers);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    internal static class CategoryExtensions
    {
        public static Category AddCategoryChild(this Category category, params Category[] categoryChildren)
        {
            foreach (var child in categoryChildren)
            {
                category.ChildCategories.Add(child);
            }

            return category;
        }
    }
}
