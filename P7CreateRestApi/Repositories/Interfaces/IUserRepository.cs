using P7CreateRestApi.Models;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<User>> FindAll();
        void Add(User user);
        User FindById(int id);
    }
}
