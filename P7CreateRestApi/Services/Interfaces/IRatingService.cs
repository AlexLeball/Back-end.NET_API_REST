using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRatingService
    {
        IEnumerable<Rating> GetAll();
        Rating? GetById(int id);
        void Add(Rating rating);
        bool Update(int id, Rating rating);
        bool Delete(int id);

    }
}
