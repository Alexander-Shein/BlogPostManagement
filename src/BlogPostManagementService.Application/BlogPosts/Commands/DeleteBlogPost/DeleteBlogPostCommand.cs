using EmpCore.Application.Commands;
using EmpCore.Domain;

namespace BlogPostManagementService.Application.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommand : Command<Result>
{
    public string DeletedBy { get; }
    public Guid BlogPostId { get; }

    public DeleteBlogPostCommand(string deletedBy, Guid blogPostId)
    {
        DeletedBy = deletedBy;
        BlogPostId = blogPostId;
    }
}
