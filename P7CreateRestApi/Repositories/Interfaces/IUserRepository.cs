using P7CreateRestApi.Models;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<User>> FindAll();
        bool Update(int id, User user);
        void Add(User user);
        User FindById(int id);
    }
}
