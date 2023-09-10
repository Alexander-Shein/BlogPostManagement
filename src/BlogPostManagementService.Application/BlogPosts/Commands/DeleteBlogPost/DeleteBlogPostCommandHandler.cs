using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, Result>
{
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;

    public DeleteBlogPostCommandHandler(IBlogPostDomainRepository blogPostDomainRepository)
    {
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
    }

    public async Task<Result> Handle(DeleteBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Ok();

        var deletedBy = AuthorId.Create(command.DeletedBy);
        if (deletedBy.IsFailure) return deletedBy;
        
        var result = blogPost.Delete(deletedBy);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);

        return Result.Ok();
    }
}