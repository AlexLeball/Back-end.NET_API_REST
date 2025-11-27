using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        public LocalDbContext DbContext { get; }
        public RatingRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<Rating> GetAll()
        {
            return DbContext.Ratings.ToList();
        }

        public Rating? GetById(int id)
        {
            return DbContext.Ratings.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Rating rating)
        {
            DbContext.Ratings.Add(rating);
            DbContext.SaveChanges();
        }

        public bool Update(int id, Rating rating)
        {
            var existing = DbContext.Ratings.Find(id);
            if (existing == null) return false;
            existing.MoodysRating = rating.MoodysRating;
            existing.SandPRating = rating.SandPRating;
            existing.FitchRating = rating.FitchRating;
            existing.OrderNumber = rating.OrderNumber;
            DbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var rating = DbContext.Ratings.Find(id);
            if (rating == null) return false;
            DbContext.Ratings.Remove(rating);
            DbContext.SaveChanges();
            return true;
        }
    }
}
