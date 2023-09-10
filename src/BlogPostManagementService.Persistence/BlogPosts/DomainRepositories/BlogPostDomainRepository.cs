using BlogPostManagementService.Domain.BlogPosts;
using EmpCore.Persistence.EntityFrameworkCore;

namespace BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;

public class BlogPostDomainRepository : IBlogPostDomainRepository
{
    private readonly AppDbContext _appDbContext;

    public BlogPostDomainRepository(AppDbContext dbContext)
    {
        _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<BlogPost?> GetByIdAsync(Guid blogPostId)
    {
        if (blogPostId == Guid.Empty) return null;
        
        return await _appDbContext.FindAsync<BlogPost>(blogPostId).ConfigureAwait(false);
    }

    public void Save(BlogPost blogPost)
    {
        _appDbContext.Add(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }

    public void Update(BlogPost blogPost)
    {
        _appDbContext.Update(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }
}