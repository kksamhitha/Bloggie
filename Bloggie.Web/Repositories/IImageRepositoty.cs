namespace Bloggie.Web.Repositories
{
    public interface IImageRepositoty
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
