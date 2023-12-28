using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        // Get method for /AdminTags/Add path
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
