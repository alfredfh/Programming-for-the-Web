using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Repositories
{
    public class CausePostCommentRepository : ICausePostCommentRepository
    {
        private readonly VoteDbContext voteDbContext;

        public CausePostCommentRepository(VoteDbContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        public async Task<CausePostComment> AddAsync(CausePostComment causePostComment)
        {
            await voteDbContext.CausePostComment.AddAsync(causePostComment);    
            await voteDbContext.SaveChangesAsync();
            return causePostComment;
        }

        public async Task<IEnumerable<CausePostComment>> GetCommentsByCauseIdAsync(Guid causePostId)
        {
            return await voteDbContext.CausePostComment.Where(x => x.CausePostId == causePostId)
                .ToListAsync();
        }
    }
}
