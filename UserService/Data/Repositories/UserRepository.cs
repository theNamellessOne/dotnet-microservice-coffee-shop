using UserService.Models;

namespace UserService.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }

    public void CreateUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        _dbContext.Users.Add(user);
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Id == id)!;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _dbContext.Users.ToList();
    }
}