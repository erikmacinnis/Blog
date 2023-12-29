using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BlogDbContext blogDbContext;

        public AdminTagsController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        // Get method for /AdminTags/Add path
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            // Mapping addTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            // Adding the tag into the db
            blogDbContext.Tags.Add(tag);
            // Must save changes to the db
            blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            // use dbContext to read the tags
            var tags = blogDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // 1st option
            //var tag = blogDbContext.Tags.Find(id);

            //2nd option
            var tag = blogDbContext.Tags.FirstOrDefault(x => x.Id == id);

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

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = blogDbContext.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                blogDbContext.SaveChanges();

                // Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            // Show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = blogDbContext.Tags.FirstOrDefault(x => x.Id == editTagRequest.Id);

            if (tag != null)
            {
                blogDbContext.Tags.Remove(tag);
                blogDbContext.SaveChanges();

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
