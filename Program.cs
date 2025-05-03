using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        using var context = new BookstoreDbContext();
        context.Database.EnsureCreated();
        context.InitializeDatabase();

        var authService = new AuthService(context);
        var bookService = new BookService(context);

        Console.WriteLine("Вход в систему");
        Console.Write("Логин: ");
        string username = Console.ReadLine();
        Console.Write("Пароль: ");
        string password = Console.ReadLine();

        if (authService.Login(username, password))
        {
            Console.WriteLine("Вход выполнен успешно");
            Console.WriteLine("\nГОЛОВНЕ МЕНЮ");
            Console.WriteLine("1. Додати книгу");
            Console.WriteLine("2. Видалити книгу");
            Console.WriteLine("3. Редагувати книгу");
            Console.WriteLine("4. Продати книгу");
            Console.WriteLine("5. Відкласти книгу для покупця");
            Console.WriteLine("6. Додати книгу до акції");
            Console.WriteLine("7. Пошук книг");
            Console.WriteLine("8. Показати список книг");
            Console.WriteLine("9. Вийти");

            Console.Write("Виберіть опцію(цифра): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    bookService.AddBook();
                    bookService.ListBooks();
                    break;
                case 2:
                    bookService.ListBooks();
                    bookService.DeleteBook();
                    bookService.ListBooks();
                    break;
                case 3:
                    bookService.ListBooks();
                    bookService.EditBook();
                    bookService.ListBooks();
                    break;
                case 4:
                    bookService.ListBooks();
                    bookService.SellBook();
                    bookService.ListBooks();
                    break;
                case 5:
                    bookService.ListBooks();
                    bookService.ReserveBook();
                    bookService.ListBooks();
                    break;
                case 6:
                    bookService.ListBooks();
                    bookService.AddBookToPromotion();
                    bookService.ListBooks();
                    break;
                case 7:
                    bookService.ListBooks();
                    bookService.SearchBooks();
                    break;
                case 8:
                    Console.WriteLine("----------------------------------------");
                    bookService.ListBooks();
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Некоректний вибір!");
                    break;
            }

        }
        else
        {
            Console.WriteLine("Неверный логин или пароль");
        }
    }
}
