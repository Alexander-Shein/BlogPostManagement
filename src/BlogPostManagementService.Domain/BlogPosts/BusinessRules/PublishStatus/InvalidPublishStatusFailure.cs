using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.PublishStatus;

public class InvalidPublishStatusFailure : Failure
{
    private const string ErrorCode = "invalid_publish_status";
    private static readonly string ErrorMessage =
        $"Invalid publish status. Possible values: ({String.Join(',', ValueObjects.PublishStatus.List)}).";

    public string Value { get; }

    public InvalidPublishStatusFailure(string value) : base( ErrorCode, ErrorMessage)
    {
        Value = value;
    }
}