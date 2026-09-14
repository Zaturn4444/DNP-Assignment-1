using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Create new post");

        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Body: ");
        string body = Console.ReadLine();

        Console.Write("User ID: ");
        int userId = Convert.ToInt32(Console.ReadLine());

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };

        Post created = await postRepository.AddAsync(post);

        Console.WriteLine($"Post created with ID: {created.Id}");
    }
}