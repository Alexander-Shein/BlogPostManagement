using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmbeddedResourse;
using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Url;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Url : SingleValueObject<string>
{
    private Url(string value) : base(value?.Trim()) { }
    
    public static Result<Url> Create(string url)
    {
        url = url?.Trim();
        
        if (string.IsNullOrWhiteSpace(url)) return EmptyUrlFailure.Instance;
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) return new InvalidUrlFailure(url);

        return new Url(url);
    }
}