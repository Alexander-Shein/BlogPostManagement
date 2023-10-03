using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Content;

public class ContentMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "content_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public ContentMaxLengthExceededFailure(int actualLength) : base(
        ErrorCode,
        $"The length of content must be {ValueObjects.Content.MaxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = ValueObjects.Content.MaxLength;
        ActualLength = actualLength;
    }
}
