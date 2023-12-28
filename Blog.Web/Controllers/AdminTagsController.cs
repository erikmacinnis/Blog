using Blog.Web.Models.ViewModels;
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

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var name = addTagRequest.Name;
            var displayName = addTagRequest.DisplayName;

            return View("Add");
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
