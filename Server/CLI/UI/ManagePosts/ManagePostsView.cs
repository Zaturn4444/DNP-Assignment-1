using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ManagePostsView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Manage posts");
        Console.WriteLine("1. Create post");
        Console.WriteLine("2. List posts");
        Console.WriteLine("3. View specific post");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            CreatePostView createPostView =
                new CreatePostView(postRepository);

            await createPostView.ShowAsync();
        }

        if (choice == "2")
        {
            ListPostsView listPostsView =
                new ListPostsView(postRepository);

            listPostsView.Show();
        }

        if (choice == "3")
        {
            SinglePostView singlePostView =
                new SinglePostView(postRepository, commentRepository);

            await singlePostView.ShowAsync();
        }
    }
}