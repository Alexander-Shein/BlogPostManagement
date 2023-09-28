using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.AuthorId;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class AuthorId : SingleValueObject<string>
{
    public const int MaxLength = 128;
    
    private AuthorId(string value) : base(value) { }
    
    public static Result<AuthorId> Create(string authorId)
    {
        authorId = authorId?.Trim();
        
        if (string.IsNullOrWhiteSpace(authorId)) return EmptyAuthorIdFailure.Instance;
        if (authorId.Length > MaxLength) return new AuthorIdMaxLengthExceededFailure(authorId.Length);

        return new AuthorId(authorId);
    }
}