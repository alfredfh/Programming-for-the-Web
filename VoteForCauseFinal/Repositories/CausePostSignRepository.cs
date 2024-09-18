using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Repositories
{

    //create the class for repo using the interface
    public class CausePostSignRepository : ICausePostSignRepository
    {
        private readonly VoteDbContext voteDbContext;

        public CausePostSignRepository(VoteDbContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        public async Task<CausePostSign> AddSignForCause(CausePostSign causePostSign)
        {
            await voteDbContext.CausePostSigns.AddAsync(causePostSign);
            await voteDbContext.SaveChangesAsync();
            return causePostSign;
        }

        public async Task<IEnumerable<CausePostSign>> GetSignsForCauseForUser(Guid causePostId)
        {
            return await voteDbContext.CausePostSigns.Where(x => x.CausePostId == causePostId)
                .ToListAsync();
        }

        //make it async
        public async Task<int> GetTotalSigns(Guid causePostId)
        {
           return await voteDbContext.CausePostSigns
                .CountAsync(x => x.CausePostId == causePostId);
        }
    }
}
