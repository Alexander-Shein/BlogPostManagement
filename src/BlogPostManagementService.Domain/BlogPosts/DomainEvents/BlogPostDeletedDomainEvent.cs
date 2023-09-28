using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostDeletedDomainEvent : DomainEvent
    {
        public Guid BlogPostId { get; }
        public AuthorId AuthorId { get; }

        internal BlogPostDeletedDomainEvent(Guid blogPostId, AuthorId authorId)
        {
            Contracts.Require(blogPostId != Guid.Empty);
            Contracts.Require(authorId != null);
            
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }
    }
}