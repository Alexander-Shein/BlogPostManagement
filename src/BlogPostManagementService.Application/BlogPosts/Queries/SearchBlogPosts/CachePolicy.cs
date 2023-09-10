using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using EmpCore.QueryStack;
using EmpCore.QueryStack.Middleware.Caching;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts
{
    public class CachePolicy : CachePolicy<SearchBlogPostsQuery, PagedList<BlogPostListItemDto>>
    {
        public override TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromSeconds(15);
    }
}