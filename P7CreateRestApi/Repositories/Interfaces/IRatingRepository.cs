using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        IEnumerable<Rating> GetAll();
        Rating? GetById(int id);
        void Add(Rating rating);
        bool Update(int id, Rating rating);
        bool Delete(int id);

    }
}
