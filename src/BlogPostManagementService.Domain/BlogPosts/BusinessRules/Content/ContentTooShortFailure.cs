using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Content;

public class ContentTooShortFailure : Failure
{
    private const string ErrorCode = "content_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public ContentTooShortFailure(int actualLength) : base(
        ErrorCode,
        $"The length of content must be {ValueObjects.Content.MinLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = ValueObjects.Content.MinLength;
        ActualLength = actualLength;
    }
}
