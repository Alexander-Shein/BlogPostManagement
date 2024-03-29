﻿using System.Collections.Immutable;
using BlogPostManagementService.Domain.BlogPosts.BusinessRules;
using BlogPostManagementService.Domain.BlogPosts.BusinessRules.PublishStatus;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class PublishStatus : SingleValueObject<string>
{
    public static readonly PublishStatus Draft = new(nameof(Draft));
    public static readonly PublishStatus Released = new(nameof(Released));

    public static readonly IReadOnlyList<PublishStatus> List = new[] { Draft, Released }.ToImmutableList();

    private PublishStatus(string value) : base(value) { }

    public static Result<PublishStatus> From(string publishStatus)
    {
        if (String.IsNullOrWhiteSpace(publishStatus)) return new InvalidPublishStatusFailure(publishStatus);

        publishStatus = publishStatus.Trim();

        var state = List.FirstOrDefault(s =>
            String.Equals(s.Value, publishStatus, StringComparison.OrdinalIgnoreCase));

        if (state == null) return new InvalidPublishStatusFailure(publishStatus);
        return state;
    }
}
