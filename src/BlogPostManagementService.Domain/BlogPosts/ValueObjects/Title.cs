using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Title;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Title : SingleValueObject<string>
{
    public const int MinLength = 3;
    public const int MaxLenght = 1000;
    private const string NotAllowedCharacters = "\r\n";

    private Title(string value) : base(value) { }

    public static Result<Title> Create(string title)
    {
        title = title?.Trim();
        
        if (string.IsNullOrWhiteSpace(title)) return EmptyTitleFailure.Instance;
        if (title.Length > MaxLenght) return new TitleMaxLengthExceededFailure(title.Length);
        if (title.Length < MinLength) return new TitleTooShortFailure(title.Length);
        if (title.Any(NotAllowedCharacters.Contains)) return NotAllowedTitleCharactersFailure.Instance;

        return Result.Ok(new Title(title));
    }
}