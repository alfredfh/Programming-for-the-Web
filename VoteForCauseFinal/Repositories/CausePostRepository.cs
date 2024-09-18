using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Repositories
{
    public class CausePostRepository : ICausePostRepository
    {
        private readonly VoteDbContext voteDbContext;

        public CausePostRepository(VoteDbContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        public async Task<CausePost> AddAsync(CausePost causePost)
        {
           await voteDbContext.AddAsync(causePost);
            await voteDbContext.SaveChangesAsync();
            return causePost;
        }

        public async Task<CausePost?> DeleteAsync(Guid id)
        {
           var existingCause = await voteDbContext.CausePosts.FindAsync(id);
        
            if (existingCause != null)
            {
                voteDbContext.CausePosts.Remove(existingCause);
                await voteDbContext.SaveChangesAsync();
                return existingCause;
            }
            return null;

        }

        public async Task<IEnumerable<CausePost>> GetAllAsync()
        {
            return await voteDbContext.CausePosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<CausePost?> GetAsync(Guid id)
        {
            return await voteDbContext.CausePosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CausePost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await voteDbContext.CausePosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<CausePost>> SearchCauses(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<CausePost>();
            }

            query = $"%{query}%";
            return await voteDbContext.CausePosts.Include(x => x.Tags)
                .Where(c => EF.Functions.Like(c.Description, query) || EF.Functions.Like(c.Heading, query))
                .ToListAsync();
        }

        public async Task<CausePost?> UpdateAsync(CausePost causePost)
        {
           var existingCause = await voteDbContext.CausePosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == causePost.Id);

            if (existingCause != null)
            {
                existingCause.Id = causePost.Id;
                existingCause.Heading = causePost.Heading;
                existingCause.ShortDescription = causePost.ShortDescription;
                existingCause.Description = causePost.Description;
                existingCause.Author = causePost.Author;
                existingCause.FeaturedImageUrl = causePost.FeaturedImageUrl;
                existingCause.UrlHandle = causePost.UrlHandle;
                existingCause.PublishedDate = causePost.PublishedDate;
                existingCause.Visible = causePost.Visible;
                existingCause.Tags = causePost.Tags;

                await voteDbContext.SaveChangesAsync();
                return existingCause;

            }
            return null;
            
        }
    


    
    }
}
