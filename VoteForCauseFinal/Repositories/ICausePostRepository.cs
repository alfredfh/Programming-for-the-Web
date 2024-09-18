using VoteForCauseFinal.Models.Domain;
namespace VoteForCauseFinal.Repositories
{
    public interface ICausePostRepository
    {
        Task<IEnumerable<CausePost>> GetAllAsync();

        Task<CausePost?> GetAsync(Guid id);

        Task<CausePost?> GetByUrlHandleAsync (string urlHandle);

        Task<CausePost> AddAsync(CausePost causePost);

        Task<CausePost?> UpdateAsync(CausePost causePost);

        Task<CausePost?> DeleteAsync(Guid id);
        Task<IEnumerable<CausePost>> SearchCauses(string query);
    }
}
