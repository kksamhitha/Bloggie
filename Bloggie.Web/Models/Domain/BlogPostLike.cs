namespace Bloggie.Web.Models.Domain
{
    public class BlogPostLike
    {
        public Guid id  { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
