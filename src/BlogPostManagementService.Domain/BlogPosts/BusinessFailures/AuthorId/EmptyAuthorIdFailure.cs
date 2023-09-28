using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.AuthorId;

public class EmptyAuthorIdFailure : Failure
{
    private const string ErrorCode = "empty_author_id";
    private static readonly string ErrorMessage = "Author Id must not be empty.";

    public static readonly EmptyAuthorIdFailure Instance = new();

    private EmptyAuthorIdFailure() : base(ErrorCode, ErrorMessage) { }
}