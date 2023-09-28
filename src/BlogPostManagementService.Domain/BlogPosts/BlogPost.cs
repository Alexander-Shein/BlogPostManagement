using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.BlogPost;
using BlogPostManagementService.Domain.BlogPosts.DomainEvents;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class BlogPost : AggregateRoot<Guid>
{
    public Author Author { get; private set; }

    public Title Title { get; private set; }
    public Content Content { get; private set; }

    public PublishStatus PublishStatus { get; private set; }
    public DateTime? PublishDateTime { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Result<BlogPost> CreateDraftBlogPost(Author author, Title title, Content content)
    {
        Contracts.Require(author != null);
        Contracts.Require(title != null);
        Contracts.Require(content != null);
        
        var now = DateTime.UtcNow;

        var blogPost = new BlogPost
        {
            Id = Guid.NewGuid(),
            Author = author,
            Title = title,
            PublishStatus = PublishStatus.Draft,
            PublishDateTime = null,
            Content = content,
            IsDeleted = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        return blogPost;
    }

    public Result Update(AuthorId updatedBy, Title? title = null, Content? content = null)
    {
        if (Author.Id != updatedBy) return new BlogPostUpdateForbiddenFailure(Id);
        if (IsDeleted) return new BlogPostIsDeletedFailure(Id);
        
        var now = DateTime.UtcNow;
        
        if (title != null && title != Title)
        {
            Title = title;
            UpdatedAt = now;
        }

        if (content != null && content != Content)
        {
            Content = content;
            UpdatedAt = now;
        }

        return Result.Ok();
    }

    public Result Publish(AuthorId publishedBy)
    {
        if (Author.Id != publishedBy) return new BlogPostUpdateForbiddenFailure(Id);
        if (IsDeleted) return new BlogPostIsDeletedFailure(Id);

        PublishStatus = PublishStatus.Released;
        PublishDateTime = UpdatedAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new BlogPostPublishedDomainEvent(Id, Author.Id, PublishDateTime.Value, Author.FeedbackEmailAddress));
        return Result.Ok();
    }
    
    public Result Delete(AuthorId deletedBy)
    {
        if (Author.Id != deletedBy) return new BlogPostUpdateForbiddenFailure(Id);
        if (IsDeleted) return Result.Ok();

        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new BlogPostDeletedDomainEvent(Id, Author.Id));
        return Result.Ok();
    }
}