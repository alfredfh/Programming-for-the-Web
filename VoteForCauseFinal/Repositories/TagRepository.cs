using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Models.Domain;


namespace VoteForCauseFinal.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly VoteDbContext voteDbContext;

        public TagRepository(VoteDbContext voteDbContext)
        {
                this.voteDbContext = voteDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await voteDbContext.Tags.AddAsync(tag);
            await voteDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var existingTag = await voteDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                voteDbContext.Tags.Remove(existingTag);
                await voteDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await voteDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return voteDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);    
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await voteDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await voteDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
