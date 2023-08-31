using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmailAddress;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class EmailAddress : SingleValueObject<string>
{
    public const int MaxLength = 256;

    private EmailAddress(string value) : base(value)
    {
    }

    public static Result<EmailAddress> Create(string emailAddress)
    {
        emailAddress = emailAddress?.Trim();
        
        if (String.IsNullOrWhiteSpace(emailAddress)) return EmptyEmailAddressFailure.Instance; 
        if (emailAddress.Length > MaxLength) return new EmailAddressMaxLengthExceededFailure(emailAddress.Length);

        // checks if there is only one '@' character
        // and it's neither the first nor the last character
        var indexAtSign = emailAddress.IndexOf('@');
        if (!(indexAtSign > 0
            && indexAtSign != emailAddress.Length - 1
            && indexAtSign == emailAddress.LastIndexOf('@')))
        {
            return new InvalidEmailAddressFailure(emailAddress);
        }

        return new EmailAddress(emailAddress.ToUpperInvariant());
    }
}
