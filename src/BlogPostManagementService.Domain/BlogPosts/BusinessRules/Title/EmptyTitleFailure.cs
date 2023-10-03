using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Title;

public class EmptyTitleFailure : Failure
{
    private const string ErrorCode = "empty_title";
    private static readonly string ErrorMessage = "Title must not be empty.";

    public static readonly EmptyTitleFailure Instance = new();

    private EmptyTitleFailure() : base(ErrorCode, ErrorMessage) { }
}
