using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            var selectedTags = new List<Tag>();
            //Map Tags from selected Tags
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //Map view model to domain model
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
                Tags = selectedTags,
            };

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}
