using Microsoft.AspNetCore.Mvc.Rendering;

namespace VoteForCauseFinal.Models.ViewModels
{
    public class AddCausePostRequest
    {
        public string Heading { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        //Display tags

        public IEnumerable<SelectListItem> Tags { get; set; }

        //collect tag

        public string[] SelectedTags { get; set; } = Array.Empty<string>(); 

    }
}
