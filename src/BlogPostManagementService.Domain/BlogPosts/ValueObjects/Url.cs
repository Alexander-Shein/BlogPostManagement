using BlogPostManagementService.Domain.BlogPosts.BusinessRules.EmbeddedResourse;
using BlogPostManagementService.Domain.BlogPosts.BusinessRules.Url;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Url : SingleValueObject<string>
{
    private Url(string value) : base(value) { }
    
    public static Result<Url> Create(string url)
    {
        url = url?.Trim();
        
        if (string.IsNullOrWhiteSpace(url)) return EmptyUrlFailure.Instance;
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) return new InvalidUrlFailure(url);

        return new Url(url);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToUpperInvariant();
    }
}