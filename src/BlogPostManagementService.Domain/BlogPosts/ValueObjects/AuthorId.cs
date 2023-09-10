using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.AuthorId;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class AuthorId : SingleValueObject<string>
{
    private AuthorId(string value) : base(value?.Trim()) { }
    
    public static Result<AuthorId> Create(string authorId)
    {
        authorId = authorId?.Trim();
        
        if (string.IsNullOrWhiteSpace(authorId)) return EmptyAuthorIdFailure.Instance;

        return new AuthorId(authorId);
    }
}