using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class Author : Entity<AuthorId>
{
    public EmailAddress FeedbackEmailAddress { get; private set; }

    public static Result<Author> Create(AuthorId id, EmailAddress feedbackEmailAddress)
    {
        Contracts.Require(id != null);
        Contracts.Require(feedbackEmailAddress != null);
        
        var author = new Author
        {
            Id = id,
            FeedbackEmailAddress = feedbackEmailAddress
        };

        return author;
    }
}