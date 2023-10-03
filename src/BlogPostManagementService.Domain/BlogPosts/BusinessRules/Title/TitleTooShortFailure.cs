using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Title;

public class TitleTooShortFailure : Failure
{
    private const string ErrorCode = "title_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public TitleTooShortFailure(int actualLength) : base(
        ErrorCode,
        $"The length of title must be {ValueObjects.Title.MinLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = ValueObjects.Title.MinLength;
        ActualLength = actualLength;
    }
}
