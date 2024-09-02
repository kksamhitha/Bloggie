using Microsoft.AspNetCore.Mvc;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

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
        [ActionName("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping addTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();

            //return View("Add");
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //USe DB Context to read the tags
            var tags = await bloggieDbContext.Tags.ToListAsync();

            return View(tags); 
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            //var Tag = bloggieDbContext.Tags.Find(id);
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Name = tag.Name,
                    Id = tag.Id,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            // Mapping editTagRequest to Tag domain model
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            //show an error notification
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);
            if(tag != null)
            {
                bloggieDbContext.Remove(tag); //No async for remove function
                await bloggieDbContext.SaveChangesAsync();
                //Show success notification
                return RedirectToAction("List");
            }
            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
