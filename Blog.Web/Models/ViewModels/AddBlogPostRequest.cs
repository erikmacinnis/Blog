﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        // This is used to display the Tags
        // Everything else are inputs from the user
        // This makes it clear on how to display the items in the list
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
