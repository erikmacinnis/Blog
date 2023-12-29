using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminBlogPostsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            // Setting the available tags in the AddBlogPostRequest
            var model = new AddBlogPostRequest
            {
                // Creating the SelectedListItem
                // Select transforms them into a selected list item
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.DisplayName,
                    // Using a unique id
                    Value = x.Id.ToString(),
                })
            };

            return View(model);
        }

        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            return RedirectToAction("Add");
        }
    }
}
