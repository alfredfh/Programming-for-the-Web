using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Repositories
{
    public interface ICausePostCommentRepository
    {

        Task<CausePostComment> AddAsync(CausePostComment causePostComment);

        Task<IEnumerable<CausePostComment>> GetCommentsByCauseIdAsync(Guid causePostId);
    }
}
