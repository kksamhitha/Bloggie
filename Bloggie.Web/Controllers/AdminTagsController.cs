using Microsoft.AspNetCore.Mvc;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggieDbContext bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext) 
        { 
           this.bloggieDbContext = bloggieDbContext;
        }    

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            // Mapping addTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            return View("Add");
        }
    }
}
