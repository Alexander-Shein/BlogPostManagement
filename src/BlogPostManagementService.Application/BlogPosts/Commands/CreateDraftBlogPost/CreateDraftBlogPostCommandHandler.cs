using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using BlogPostManagementService.Domain.BlogPosts;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;

public class CreateDraftBlogPostCommandHandler : IRequestHandler<CreateDraftBlogPostCommand, Result<Guid>>
{
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;

    public CreateDraftBlogPostCommandHandler(IBlogPostDomainRepository blogPostDomainRepository)
    {
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
    }

    public async Task<Result<Guid>> Handle(CreateDraftBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var author = BuildAuthor(command.AuthorId, command.FeedbackEmailAddress);
        var title = Title.Create(command.Title);
        var content = BuildContent(command.Content, command.EmbeddedResources);
        
        var result = Result.Combine(author, title, content);
        if (result.IsFailure) return Result.Fail<Guid>(result.Failures);

        var blogPost = BlogPost.CreateDraftBlogPost(author, title, content);
        if (blogPost.IsFailure) return Result.Fail<Guid>(result.Failures);
        
        _blogPostDomainRepository.Save(blogPost);
        
        return Result.Ok(blogPost.Value.Id);
    }

    private static Result<Author> BuildAuthor(string authorId, string feedbackEmailAddress)
    {
        var authorIdResult = AuthorId.Create(authorId);
        var feedbackEmailAddressResult = EmailAddress.Create(feedbackEmailAddress);
        
        var result = Result.Combine(authorIdResult, feedbackEmailAddressResult);
        if (result.IsFailure) return Result.Fail<Author>(result.Failures);
        
        var author = Author.Create(authorIdResult, feedbackEmailAddressResult);
        return author;
    }

    private static Result<Content> BuildContent(string text, IEnumerable<EmbeddedResourceDto> embeddedResources)
    {
        var embeddedResourcesDomain = Enumerable.Empty<EmbeddedResource>();
        
        if (embeddedResources.Any())
        {
            var embeddedResourcesResult = embeddedResources
                .Select(er => EmbeddedResource.Create(new Uri(er.Url), er.Caption))
                .ToArray();

            var result = Result.Combine(embeddedResourcesResult);
            if (result.IsFailure) return Result.Fail<Content>(result.Failures);
            embeddedResourcesDomain = embeddedResourcesResult.Select(er => er.Value);
        }
        
        var content = Content.Create(text, embeddedResourcesDomain);
        return content;
    }
}