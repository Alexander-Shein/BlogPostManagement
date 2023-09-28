using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.AuthorId;

public class AuthorIdMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "author_id_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public AuthorIdMaxLengthExceededFailure(int actualLength) : base(
        ErrorCode,
        $"The length of author id must be {ValueObjects.AuthorId.MaxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = ValueObjects.AuthorId.MaxLength;
        ActualLength = actualLength;
    }
}