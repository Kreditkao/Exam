using System;
using System.Linq;

public class BookService
{
    private readonly BookstoreDbContext _context;

    public BookService(BookstoreDbContext context)
    {
        _context = context;
    }

    public void AddBook()
    {
        Console.WriteLine("Добавление новой книги");

        Console.Write("Название: ");
        string title = Console.ReadLine();

        Console.Write("Автор: ");
        string author = Console.ReadLine();

        Console.Write("Издательство: ");
        string publisher = Console.ReadLine();

        Console.Write("Количество страниц: ");
        int pageCount = Convert.ToInt32(Console.ReadLine());

        Console.Write("Жанр: ");
        string genre = Console.ReadLine();

        Console.Write("Год издания: ");
        int year = Convert.ToInt32(Console.ReadLine());

        Console.Write("ISBN (13 символов): ");
        string isbn = Console.ReadLine();

        Console.Write("Себестоимость: ");
        decimal costPrice = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Цена продажи: ");
        decimal salePrice = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Количество на складе: ");
        int stockQuantity = Convert.ToInt32(Console.ReadLine());

        Console.Write("Скидка (%) (если нет, введите 0): ");
        decimal? discountPercentage = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Книга зарезервирована? (yes/no): ");
        bool isReserved = Console.ReadLine().ToLower() == "yes";

        string? reservedBy = null;
        if (isReserved)
        {
            Console.Write("Введите имя покупателя: ");
            reservedBy = Console.ReadLine();
        }

        Console.Write("Книга участвует в акции? (yes/no): ");
        bool isDiscounted = Console.ReadLine().ToLower() == "yes";

        string? promotionName = null;
        if (isDiscounted)
        {
            Console.Write("Введите название акции: ");
            promotionName = Console.ReadLine();
        }

        var book = new Book
        {
            Title = title,
            Author = author,
            Publisher = publisher,
            PageCount = pageCount,
            Genre = genre,
            Year = year,
            ISBN = isbn,
            CostPrice = costPrice,
            SalePrice = salePrice,
            StockQuantity = stockQuantity,
            DiscountPercentage = discountPercentage,
            IsReserved = isReserved,
            ReservedBy = reservedBy,
            IsDiscounted = isDiscounted,
            PromotionName = promotionName
        };

        _context.Books.Add(book);
        _context.SaveChanges();
        Console.WriteLine($"Книга \"{title}\" успешно добавлена!");
    }

    public void ListBooks()
    {
        var books = _context.Books.ToList();
        Console.WriteLine("Список книг:");

        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.BookId}");
            Console.WriteLine($"Название: {book.Title}");
            Console.WriteLine($"Автор: {book.Author}");
            Console.WriteLine($"Издательство: {book.Publisher}");
            Console.WriteLine($"Страниц: {book.PageCount}");
            Console.WriteLine($"Жанр: {book.Genre}");
            Console.WriteLine($"Год издания: {book.Year}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine($"Себестоимость: {book.CostPrice}");
            Console.WriteLine($"Цена продажи: {book.SalePrice}");
            Console.WriteLine($"Остаток на складе: {book.StockQuantity}");
            Console.WriteLine($"Скидка: {book.DiscountPercentage ?? 0}%");
            Console.WriteLine($"Акция: {book.PromotionName ?? "Нет"}");
            Console.WriteLine($"Зарезервирована: {(book.IsReserved ? $"Да (для {book.ReservedBy})" : "Нет")}");
            Console.WriteLine("----------------------------------------");
        }
    }

    public void DeleteBook()
    {
        Console.Write("Введите ID книги для удаления: ");
        int bookId = Convert.ToInt32(Console.ReadLine());

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            Console.WriteLine("Книга успешно удалена.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    public void EditBook()
    {
        Console.Write("Введите ID книги для редактирования: ");
        int bookId = Convert.ToInt32(Console.ReadLine());

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book == null)
        {
            Console.WriteLine("Книга не найдена.");
            return;
        }

        Console.Write("Новое название (оставьте пустым для пропуска): ");
        string newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle)) book.Title = newTitle;

        Console.Write("Новый автор: ");
        string newAuthor = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newAuthor)) book.Author = newAuthor;

        Console.Write("Новая цена: ");
        decimal newPrice;
        if (decimal.TryParse(Console.ReadLine(), out newPrice)) book.SalePrice = newPrice;

        _context.SaveChanges();
        Console.WriteLine("Книга успешно обновлена.");
    }

    public void SellBook()
    {
        Console.Write("Введите ID книги для продажи: ");
        int bookId = Convert.ToInt32(Console.ReadLine());

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book != null && book.StockQuantity > 0) 
        {
            book.StockQuantity--;
            _context.SaveChanges();
            Console.WriteLine($"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine($" ");
            Console.WriteLine($"Книга \"{book.Title}\" продана. Остаток на складе: {book.StockQuantity}");
            Console.WriteLine($" ");
            Console.WriteLine($"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
        }
        else
        {
            Console.WriteLine("Книга не найдена или отсутствует на складе.");
        }
    }

    public void ReserveBook()
    {
        Console.Write("Введите ID книги для резервирования: ");
        int bookId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите имя покупателя: ");
        string customerName = Console.ReadLine();

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book != null && book.StockQuantity > 0)
        {
            book.IsReserved = true;
            book.ReservedBy = customerName;
            _context.SaveChanges();
            Console.WriteLine($"Книга \"{book.Title}\" зарезервирована для {customerName}.");
        }
        else
        {
            Console.WriteLine("Книга не найдена или отсутствует на складе.");
        }
    }

    public void AddBookToPromotion()
    {
        Console.Write("Введите ID книги для добавления в акцию: ");
        int bookId = Convert.ToInt32(Console.ReadLine());

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book != null)
        {
            Console.Write("Введите название акции: ");
            string promotionName = Console.ReadLine();

            Console.Write("Введите процент скидки (например, 10 для 10%): ");
            decimal discountPercentage = Convert.ToDecimal(Console.ReadLine());

            book.IsDiscounted = true;
            book.DiscountPercentage = discountPercentage;
            book.PromotionName = promotionName;

            _context.SaveChanges();

            Console.WriteLine($"Книга \"{book.Title}\" теперь участвует в акции \"{promotionName}\" с {discountPercentage}% скидкой.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }


    public void SearchBooks()
    {
        Console.Write("Введите параметр поиска (название, автор, жанр): ");
        string searchQuery = Console.ReadLine();

        var books = _context.Books
            .Where(b => b.Title.Contains(searchQuery) || b.Author.Contains(searchQuery) || b.Genre.Contains(searchQuery))
            .ToList();

        if (books.Any())
        {
            Console.WriteLine("Найденные книги:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} ({book.Author}), Жанр: {book.Genre}, Цена: {book.SalePrice}");
            }
        }
        else
        {
            Console.WriteLine("Книги не найдены.");
        }
    }

}
