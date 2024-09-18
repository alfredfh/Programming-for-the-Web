using Microsoft.AspNetCore.Identity;

namespace VoteForCauseFinal.Repositories
{
    public interface IUserRepository
    {

        Task <IEnumerable<IdentityUser>> GetAll();

    }
}
