using System.Linq;

public class AuthService
{
    private readonly BookstoreDbContext _context;

    public AuthService(BookstoreDbContext context)
    {
        _context = context;
    }

    public bool Login(string username, string password)
    {
        return _context.Users.Any(u => u.Name == username && u.Password == password);
    }
}
