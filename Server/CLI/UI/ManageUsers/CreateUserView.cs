using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Create new user");

        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            return;
        }

        IQueryable<User> users = userRepository.GetManyAsync();

        if (users.Any(u => u.Username == username))
        {
            Console.WriteLine("Username is already taken.");
            return;
        }

        User user = new User
        {
            Username = username,
            Password = password
        };

        User created = await userRepository.AddAsync(user);

        Console.WriteLine($"User created with ID: {created.Id}");
    }
}