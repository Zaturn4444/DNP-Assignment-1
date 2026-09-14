using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public CreateCommentView(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Add comment");

        Console.Write("Body: ");
        string body = Console.ReadLine();

        Console.Write("User ID: ");
        int userId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Post ID: ");
        int postId = Convert.ToInt32(Console.ReadLine());

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Body cannot be empty.");
            return;
        }

        if (!userRepository.GetManyAsync().Any(u => u.Id == userId))
        {
            Console.WriteLine("User does not exist.");
            return;
        }

        if (!postRepository.GetManyAsync().Any(p => p.Id == postId))
        {
            Console.WriteLine("Post does not exist.");
            return;
        }

        Comment comment = new Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };

        Comment created = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment created with ID: {created.Id}");
    }
}