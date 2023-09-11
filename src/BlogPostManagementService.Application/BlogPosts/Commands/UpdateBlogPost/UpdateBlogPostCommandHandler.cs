using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.Failures;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.UpdateBlogPost;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, Result>
{
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;

    public UpdateBlogPostCommandHandler(IBlogPostDomainRepository blogPostDomainRepository)
    {
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
    }

    public async Task<Result> Handle(UpdateBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Fail(ResourceNotFoundFailure.Instance);

        var updatedBy = AuthorId.Create(command.UpdatedBy);
        if (updatedBy.IsFailure) return updatedBy;
        
        Title? newTitle = null;
        if (command.Title != null)
        {
            var title = Title.Create(command.Title);
            if (title.IsFailure) return title;
            newTitle = title;
        }

        Content? newContent = null;
        if (command.Content != null || command.EmbeddedResources != null)
        {
            var text = command.Content ?? blogPost.Content.Text;
            Result<Content> content;

            if (command.EmbeddedResources == null)
            {
                content = Content.Create(text, blogPost.Content.EmbeddedResources);
            }
            else
            {
                content = BuildContent(text, command.EmbeddedResources);
            }
            if (content.IsFailure) return content;
            newContent = content;
        }

        var result = blogPost.Update(updatedBy, newTitle, newContent);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);

        return Result.Ok();
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