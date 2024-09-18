namespace VoteForCauseFinal.Models.Domain
{
    public class CausePostComment
    {

        public Guid id { get; set; }

        public string Description { get; set; }

        public Guid CausePostId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
