using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private LocalDbContext DbContext { get; }

        public UserRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<User>> FindAll()
        {
            return await DbContext.Users.ToListAsync();
        }

        public bool Update(int id, User user)
        {
            var existing = DbContext.Users.Find(id);
            if (existing == null) return false;
            existing.UserName = user.UserName;
            existing.Fullname = user.Fullname;
            existing.Password = user.Password;
            existing.Role = user.Role;
            DbContext.SaveChanges();
            return true;
        }

        public void Add(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public User FindById(int id)
        {
            return DbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}