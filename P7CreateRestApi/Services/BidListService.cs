using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        public LocalDbContext DbContext { get; }

        public BidListService(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public BidList? GetById(int id)
        {
            return DbContext.Bids.FirstOrDefault(c => c.BidListId == id);
        }
        public IEnumerable<BidList> GetAll()
        {
            return DbContext.Bids.ToList();
        }

        public void Add(BidList bid)
        {
            DbContext.Bids.Add(bid);
            DbContext.SaveChanges();
        }

        public bool Update(int id, BidList bid)
        {
            var existing = DbContext.Bids.Find(id);
            if (existing == null) return false;
            existing.Account = bid.Account;
            existing.BidType = bid.BidType;
            existing.BidQuantity = bid.BidQuantity;
            existing.AskQuantity = bid.AskQuantity;
            existing.Bid = bid.Bid;
            existing.Ask = bid.Ask;
            existing.DealName = bid.DealName;
            existing.DealType = bid.DealType;
            existing.SourceListId = bid.SourceListId;
            existing.Side = bid.Side;
            DbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var bid = DbContext.Bids.Find(id);
            if (bid == null) return false;
            DbContext.Bids.Remove(bid);
            DbContext.SaveChanges();
            return true;
        }
    }
}
