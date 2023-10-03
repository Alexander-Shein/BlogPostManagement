using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.Url;

public class InvalidUrlFailure : Failure
{
    private const string ErrorCode = "invalid_url";
    private const string ErrorMessage = "Url has invalid format.";

    public string Value { get; }

    public InvalidUrlFailure(string value) : base(ErrorCode, ErrorMessage)
    {
        Value = value;
    }
}