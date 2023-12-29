using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        // BlogDbContext is found because an instance is found because we injected and instance in the program.cs
        // The controller is only called when needed and by that time it can find the instance or BlogDbContext
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // Get method for /AdminTags/Add path
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping addTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            tags.Reverse();

            return View(tags);
        }

        // Called by list.cshtml file
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // 1st option
            //var tag = blogDbContext.Tags.Find(id);

            //2nd option
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        // 
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                // Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            // Show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // Show a success notification
                return RedirectToAction("List");
            }

            // Show a error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        // Can be replaced with this method
        //[HttpPost]
        //// Will make the action visible to AdminTags/Add url so we can use it in the add.cshtml page
        //[ActionName("Add")]
        //public IActionResult SubmitTag()
        //{
        //    // This must match the name params in the form
        //    //var name = Request.Form["name"];
        //    // var displayName = Request.Form["displayName"];

        //    // Necessary because function name doesn't match a view
        //    return View("Add");
        //}
    }
}
