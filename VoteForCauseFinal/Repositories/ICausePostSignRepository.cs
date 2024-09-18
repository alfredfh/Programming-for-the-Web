using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Repositories
{

    //first we create the interface for the repo
    public interface ICausePostSignRepository
    {

        Task<int> GetTotalSigns(Guid causePostId);

        Task<CausePostSign> AddSignForCause (CausePostSign causePostSign);
    
        //get all the signs back to be able to see if a user has already signed
        
        Task<IEnumerable<CausePostSign>>GetSignsForCauseForUser(Guid causePostId);

        

    }
}
