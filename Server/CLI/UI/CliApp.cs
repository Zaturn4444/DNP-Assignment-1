using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CliApp(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("1. Manage users");
        Console.WriteLine("2. Manage posts");
        Console.WriteLine("3. Add comment");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            ManageUsersView manageUsersView =
                new ManageUsersView(userRepository);

            await manageUsersView.ShowAsync();
        }

        if (choice == "2")
        {
            ManagePostsView managePostsView =
                new ManagePostsView(
                    postRepository,
                    commentRepository);

            await managePostsView.ShowAsync();
        }

        if (choice == "3")
        {
            CreateCommentView createCommentView =
                new CreateCommentView(
                    commentRepository,
                    userRepository,
                    postRepository);

            await createCommentView.ShowAsync();
        }
    }
}