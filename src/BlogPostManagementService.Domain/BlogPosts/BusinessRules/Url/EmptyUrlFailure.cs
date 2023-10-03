using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Url;

public class EmptyUrlFailure : Failure
{
    private const string ErrorCode = "empty__url";
    private static readonly string ErrorMessage = "Url must not be empty.";

    public static readonly EmptyUrlFailure Instance = new();

    private EmptyUrlFailure() : base(ErrorCode, ErrorMessage) { }
}
