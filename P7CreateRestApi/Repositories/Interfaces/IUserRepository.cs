using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> FindAll();
        Task<User> FindByIdAsync(string id);
        Task<IdentityResult> AddAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
    }
}
