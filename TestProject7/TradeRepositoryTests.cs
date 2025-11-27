using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace TestProject7
{
    [TestClass]
    public sealed class TradeRepositoryTests
    {
        private static LocalDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(dbName) // Requires Microsoft.EntityFrameworkCore.InMemory
                .Options;
            return new LocalDbContext(options);
        }

        private static Trade MakeTrade(int id = 0, string account = "ACC", string trader = "Trader")
        {
            return new Trade
            {
                TradeId = id,
                Account = account,
                AccountType = "TypeA",
                BuyQuantity = 10,
                SellQuantity = 5,
                BuyPrice = 100,
                SellPrice = 105,
                Benchmark = "Bench",
                TradeSecurity = "Sec",
                TradeStatus = "Status",
                Trader = trader,
                Book = "Book1",
                TradeDate = DateTime.Now,
                CreationName = "Creator",
                CreationDate = DateTime.Now,
                RevisionDate = DateTime.Now,
                DealName = "Deal",
                DealType = "DealType",
                SourceListId = "SRC",
                Side = "Buy"
            };
        }

        [TestMethod]
        public void GetAll_ReturnsAllTrades()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Trades.Add(MakeTrade(1));
            ctx.Trades.Add(MakeTrade(2));
            ctx.SaveChanges();

            var repo = new TradeRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(t => t.TradeId == 1));
            Assert.IsTrue(all.Any(t => t.TradeId == 2));
        }

        [TestMethod]
        public void GetById_ReturnsTrade_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Trades.Add(MakeTrade(42, "ACC42", "Trader42"));
            ctx.SaveChanges();

            var repo = new TradeRepository(ctx);
            var found = repo.GetById(42);

            Assert.IsNotNull(found);
            Assert.AreEqual("ACC42", found.Account);
            Assert.AreEqual("Trader42", found.Trader);
        }

        [TestMethod]
        public void Add_AddsTradeToDatabase()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new TradeRepository(ctx);

            var trade = MakeTrade();
            repo.Add(trade);

            var all = ctx.Trades.ToList();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual(trade.Account, all[0].Account);
        }

        [TestMethod]
        public void Update_ReturnsTrueAndUpdates_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Trades.Add(MakeTrade(5, "Before"));
            ctx.SaveChanges();

            var repo = new TradeRepository(ctx);
            var updated = MakeTrade(5, "After");
            var result = repo.Update(5, updated);

            Assert.IsTrue(result);
            var persisted = ctx.Trades.Find(5);
            Assert.AreEqual("After", persisted.Account);
        }

        [TestMethod]
        public void Update_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new TradeRepository(ctx);

            var result = repo.Update(999, MakeTrade(999));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_ReturnsTrueAndRemoves_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Trades.Add(MakeTrade(7));
            ctx.SaveChanges();

            var repo = new TradeRepository(ctx);
            var result = repo.Delete(7);

            Assert.IsTrue(result);
            Assert.IsNull(ctx.Trades.Find(7));
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new TradeRepository(ctx);

            var result = repo.Delete(12345);
            Assert.IsFalse(result);
        }
    }
}
