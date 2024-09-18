using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Models.ViewModels
{
    public class CauseDetailsViewModel
    {

        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public int TotalSigns { get; set; }

        public bool Signed { get; set; }

        public string CommentDescription { get; set; }

        //just to disaply comments
        public IEnumerable<CauseComment> Comments { get; set; }




    }
}
