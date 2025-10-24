using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private LocalDbContext DbContext { get; }
        public TradeRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IEnumerable<Trade> GetAll()
        {
            return DbContext.Trades.ToList();
        }
        public Trade? GetById(int id)
        {
            return DbContext.Trades.FirstOrDefault(t => t.TradeId == id);
        }
        public void Add(Trade trade)
        {
            DbContext.Trades.Add(trade);
            DbContext.SaveChanges();
        }
        public bool Update(int id, Trade trade)
        {
            var existing = DbContext.Trades.Find(id);
            if (existing == null) return false;
            existing.Account = trade.Account;
            existing.AccountType = trade.AccountType;
            existing.BuyQuantity = trade.BuyQuantity;
            existing.SellQuantity = trade.SellQuantity;
            existing.BuyPrice = trade.BuyPrice;
            existing.SellPrice = trade.SellPrice;
            existing.Benchmark = trade.Benchmark;
            existing.TradeSecurity = trade.TradeSecurity;
            existing.TradeStatus = trade.TradeStatus;
            existing.Trader = trade.Trader;
            existing.Book = trade.Book;
            existing.TradeDate = trade.TradeDate;
            existing.CreationName = trade.CreationName;
            existing.CreationDate = trade.CreationDate;
            existing.RevisionDate = trade.RevisionDate;
            existing.DealName = trade.DealName;
            existing.DealType = trade.DealType;
            existing.SourceListId = trade.SourceListId;
            existing.Side = trade.Side;     
        DbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var trade = DbContext.Trades.Find(id);
            if (trade == null) return false;
            DbContext.Trades.Remove(trade);
            DbContext.SaveChanges();
            return true;
        }
    }
}
