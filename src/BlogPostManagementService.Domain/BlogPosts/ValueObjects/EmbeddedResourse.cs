using BlogPostManagementService.Domain.BlogPosts.BusinessRules.EmbeddedResourse;
using BlogPostManagementService.Domain.BlogPosts.BusinessRules.Url;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class EmbeddedResource : ValueObject
{
    public Url Url { get; }
    public string Caption { get; }

    private EmbeddedResource(Url url, string caption)
    {
        Url = url;
        Caption = caption;
    }

    public static Result<EmbeddedResource> Create(Url url, string caption)
    {
        if (url == null) return EmptyUrlFailure.Instance;
        if (String.IsNullOrEmpty(caption)) return EmptyCaptionFailure.Instance;

        return new EmbeddedResource(url, caption.Trim());
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Url;
        yield return Caption;
    }
}