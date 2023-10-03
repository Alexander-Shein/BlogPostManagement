using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Title;

public class TitleMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "title_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public TitleMaxLengthExceededFailure(int actualLength) : base(
        ErrorCode,
        $"The length of title must be {ValueObjects.Title.MaxLenght} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = ValueObjects.Title.MaxLenght;
        ActualLength = actualLength;
    }
}
