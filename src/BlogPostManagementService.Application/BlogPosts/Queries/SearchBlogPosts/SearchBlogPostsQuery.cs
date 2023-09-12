using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using EmpCore.QueryStack;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts;

public class SearchBlogPostsQuery : Query<PagedList<BlogPostListItemDto>>
{
    public int PageSize { get; } = 100;
    public int PageNumber { get; } = 1;
    public string SortField { get; } = nameof(BlogPostListItemDto.CreatedAt);
    public SortDir SortDir { get; } = SortDir.Desc;
        
    public SearchBlogPostsQuery(int? pageSize, int? pageNumber, string? sortField, SortDir? sortDir)
    {
        if (pageSize != null) PageSize = pageSize.Value;
        if (pageNumber != null) PageNumber = pageNumber.Value;
        if (sortField != null) SortField = sortField;
        if (sortDir != null) SortDir = sortDir.Value;
    }
}