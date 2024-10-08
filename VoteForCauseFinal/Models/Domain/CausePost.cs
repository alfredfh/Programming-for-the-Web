﻿namespace VoteForCauseFinal.Models.Domain
{
    public class CausePost
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

        //navigation properties
        public ICollection<Tag> Tags { get; set; }
    
        public ICollection<CausePostSign> Signs { get; set; }
    
        public ICollection<CausePostComment> Comments { get; set; }
    
    }
}
