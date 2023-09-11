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
        var embeddedResourcesDomain = new List<EmbeddedResource>();
        
        if (embeddedResources.Any())
        {
            foreach (var embeddedResource in embeddedResources)
            {
                var url = Url.Create(embeddedResource.Url);
                if (url.IsFailure) return Result.Fail<Content>(url.Failures);

                var er = EmbeddedResource.Create(url, embeddedResource.Caption);
                if (er.IsFailure) return Result.Fail<Content>(er.Failures);
                
                embeddedResourcesDomain.Add(er);
            }
        }
        
        var content = Content.Create(text, embeddedResourcesDomain);
        return content;
    }
}