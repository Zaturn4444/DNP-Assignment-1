using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void Show()
    {
        Console.WriteLine("Posts:");

        IQueryable<Post> posts = postRepository.GetManyAsync();

        foreach (Post post in posts)
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}");
        }
    }
}