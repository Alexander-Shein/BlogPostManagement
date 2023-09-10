using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class Author : Entity<AuthorId>
{
    public EmailAddress FeedbackEmailAddress { get; private set; }

    public static Result<Author> Create(AuthorId id, EmailAddress feedbackEmailAddress)
    {
        var author = new Author
        {
            Id = id ?? throw new ArgumentNullException(nameof(id)),
            FeedbackEmailAddress = feedbackEmailAddress ?? throw new ArgumentNullException(nameof(feedbackEmailAddress))
        };

        return author;
    }
}