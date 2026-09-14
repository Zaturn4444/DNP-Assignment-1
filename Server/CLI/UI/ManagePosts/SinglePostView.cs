using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Post ID: ");
        int postId = Convert.ToInt32(Console.ReadLine());

        Post post = await postRepository.GetSingleAsync(postId);

        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine("Comments:");

        IQueryable<Comment> comments = commentRepository.GetManyAsync();

        foreach (Comment comment in comments)
        {
            if (comment.PostId == postId)
            {
                Console.WriteLine(comment.Body);
            }
        }
    }
}