using EmpCore.QueryStack;

namespace BlogPostManagementService.WebApi.BlogPosts.Models;

public class SearchBlogPostsInputModel
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SortField { get; set; }
    public SortDir SortDir { get; set; }
}