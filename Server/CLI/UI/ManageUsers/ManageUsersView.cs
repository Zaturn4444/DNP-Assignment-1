using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Manage users");
        Console.WriteLine("1. Create user");
        Console.WriteLine("2. List users");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            CreateUserView createUserView = new CreateUserView(userRepository);
            await createUserView.ShowAsync();
        }

        if (choice == "2")
        {
            ListUsersView listUsersView = new ListUsersView(userRepository);
            listUsersView.Show();
        }
    }
}