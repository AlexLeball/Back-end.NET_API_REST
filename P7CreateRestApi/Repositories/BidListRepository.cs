using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository
    {
        public LocalDbContext DbContext { get; }

        public BidListRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(BidList bid)
        {
            DbContext.Bids.Add(bid);
            DbContext.SaveChanges();
        }

        public BidList FindById(int id)
        {
            return DbContext.Bids.FirstOrDefault(b => b.BidListId == id);
        }

        public async Task<List<BidList>> FindAll()
        {
            return await DbContext.Bids.ToListAsync();
        }
    }
}
