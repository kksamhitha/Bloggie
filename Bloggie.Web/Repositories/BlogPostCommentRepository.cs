using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostcomment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(blogPostcomment);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostcomment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdIdAsync(Guid blogPostId)
        {
           return await bloggieDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
