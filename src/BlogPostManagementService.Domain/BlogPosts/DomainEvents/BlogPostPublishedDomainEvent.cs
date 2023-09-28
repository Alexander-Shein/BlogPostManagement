using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostPublishedDomainEvent : DomainEvent
    {
        public Guid BlogPostId { get; }
        public AuthorId AuthorId { get; }
        public DateTime PublishDateTime { get; }
        public EmailAddress FeedbackEmailAddress { get; }

        internal BlogPostPublishedDomainEvent(
            Guid blogPostId, AuthorId authorId, DateTime publishDateTime, EmailAddress feedbackEmailAddress)
        {
            Contracts.Require(BlogPostId != Guid.Empty);
            Contracts.Require(authorId != null);
            Contracts.Require(feedbackEmailAddress != null);
            
            BlogPostId = blogPostId;
            AuthorId = authorId;
            PublishDateTime = publishDateTime;
            FeedbackEmailAddress = feedbackEmailAddress;
        }
    }
}