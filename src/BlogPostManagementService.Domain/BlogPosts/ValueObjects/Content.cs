using BlogPostManagementService.Domain.BlogPosts.BusinessRules.Content;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Content : ValueObject
{
    public const int MinLength = 100;
    public const int MaxLength = 100_000;
    private static readonly IEnumerable<string> BlacklistedWords = new List<string> { "Word1", "Word2", "Word3" };

    public string Text { get; }
    public IReadOnlyList<EmbeddedResource> EmbeddedResources => _embeddedResources.ToList();
    private List<EmbeddedResource> _embeddedResources;

    private Content(string text) : this(text, Enumerable.Empty<EmbeddedResource>()) { }
    
    private Content(string text, IEnumerable<EmbeddedResource> embeddedResources)
    {
        Text = text;
        _embeddedResources = embeddedResources.ToList();
    }

    public static Result<Content> Create(string text, IEnumerable<EmbeddedResource> embeddedResources)
    {
        text = text?.Trim();
        
        if (string.IsNullOrWhiteSpace(text)) return EmptyContentFailure.Instance;
        if (text.Length > MaxLength) return new ContentMaxLengthExceededFailure(text.Length);
        if (text.Length < MinLength) return new ContentTooShortFailure(text.Length);

        text = HideBlacklistedWords(text);

        return new Content(text, embeddedResources);
    }
    
    private static string HideBlacklistedWords(string text)
    {
        foreach (var blackListedWord in BlacklistedWords)
        {
            text = text.Replace(blackListedWord, "***", StringComparison.OrdinalIgnoreCase);
        }

        return text;
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Text;
        foreach (var embeddedResource in EmbeddedResources)
        {
            yield return embeddedResource;
        }
    }
}