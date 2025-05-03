using Microsoft.EntityFrameworkCore;
using System.Linq;

public class BookstoreDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-KGIM8M1\\SQLEXPRESS;Database=BookstoreDB;Trusted_Connection=True;Encrypt=False;",
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
    }

    public void InitializeDatabase()
    {
        if (!Users.Any())
        {
            Users.Add(new User { Name = "admin", Password = "1234", IsAdmin = true });
            SaveChanges();
        }

        if (!Books.Any())
        {
            Books.AddRange(new[]
            {
                new Book { Title = "1984", Author = "George Orwell", Publisher = "Secker & Warburg", PageCount = 328, Genre = "Dystopian", Year = 1949, ISBN = "9780451524935", CostPrice = 10.0M, SalePrice = 15.0M, StockQuantity = 20 },
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Publisher = "Scribner", PageCount = 180, Genre = "Classic", Year = 1925, ISBN = "9780743273565", CostPrice = 12.0M, SalePrice = 20.5M, StockQuantity = 10 },
                new Book { Title = "Brave New World", Author = "Aldous Huxley", Publisher = "Chatto & Windus", PageCount = 311, Genre = "Science Fiction", Year = 1932, ISBN = "9780060850524", CostPrice = 11.0M, SalePrice = 18.0M, StockQuantity = 15 }
            });

            SaveChanges();
        }
    }
}
