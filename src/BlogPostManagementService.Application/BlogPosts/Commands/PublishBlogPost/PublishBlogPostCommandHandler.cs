using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.Failures;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.PublishBlogPost;

public class PublishBlogPostCommandHandler : IRequestHandler<PublishBlogPostCommand, Result>
{
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;

    public PublishBlogPostCommandHandler(IBlogPostDomainRepository blogPostDomainRepository)
    {
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
    }

    public async Task<Result> Handle(PublishBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Fail(ResourceNotFoundFailure.Instance);

        var publishedBy = AuthorId.Create(command.PublishedBy);
        if (publishedBy.IsFailure) return publishedBy;
        
        var result = blogPost.Publish(publishedBy);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);

        return Result.Ok();
    }
}