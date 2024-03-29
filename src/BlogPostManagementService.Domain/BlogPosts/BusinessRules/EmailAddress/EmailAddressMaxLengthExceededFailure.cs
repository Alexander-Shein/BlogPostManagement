﻿using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessRules.EmailAddress;

public class EmailAddressMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "email_address_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public EmailAddressMaxLengthExceededFailure(int actualLength) : base(
        ErrorCode,
        $"The length of email must be {ValueObjects.EmailAddress.MaxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = ValueObjects.EmailAddress.MaxLength;
        ActualLength = actualLength;
    }
}
