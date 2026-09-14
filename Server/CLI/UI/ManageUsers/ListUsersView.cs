using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void Show()
    {
        Console.WriteLine("Users:");

        IQueryable<User> users = userRepository.GetManyAsync();

        foreach (User user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}");
        }
    }
}