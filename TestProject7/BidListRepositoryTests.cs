using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace TestProject7
{
    [TestClass]
    public sealed class BidListRepositoryTests
    {
        private static LocalDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new LocalDbContext(options);
        }

        private static BidList MakeBid(int id = 0, string account = "ACC", string bidType = "Type")
        {
            return new BidList
            {
                BidListId = id,
                Account = account,
                BidType = bidType,
                BidQuantity = 10,
                AskQuantity = 5,
                Bid = 1.23,
                Ask = 1.25,
                DealName = "Deal",
                DealType = "DealType",
                SourceListId = "SRC",
                Side = "Buy",
                Trader = "Trader"
            };
        }

        [TestMethod]
        public void GetAll_ReturnsAllBids()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Bids.Add(MakeBid(1));
            ctx.Bids.Add(MakeBid(2));
            ctx.SaveChanges();

            var repo = new BidListRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(b => b.BidListId == 1));
            Assert.IsTrue(all.Any(b => b.BidListId == 2));
        }

        [TestMethod]
        public void GetById_ReturnsBid_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Bids.Add(MakeBid(42, "A1"));
            ctx.SaveChanges();

            var repo = new BidListRepository(ctx);
            var found = repo.GetById(42);

            Assert.IsNotNull(found);
            Assert.AreEqual("A1", found.Account);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new BidListRepository(ctx);

            var found = repo.GetById(999);

            Assert.IsNull(found);
        }

        [TestMethod]
        public void Add_AddsBidToDatabase()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new BidListRepository(ctx);

            var bid = MakeBid(0, "NewAcc");
            repo.Add(bid);

            var all = ctx.Bids.ToList();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("NewAcc", all[0].Account);
        }

        [TestMethod]
        public void Update_ReturnsTrueAndUpdates_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Bids.Add(MakeBid(5, "Before"));
            ctx.SaveChanges();

            var repo = new BidListRepository(ctx);
            var updated = MakeBid(5, "After");
            var result = repo.Update(5, updated);

            Assert.IsTrue(result);
            var persisted = ctx.Bids.Find(5);
            Assert.IsNotNull(persisted);
            Assert.AreEqual("After", persisted.Account);
        }

        [TestMethod]
        public void Update_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new BidListRepository(ctx);
            var updated = MakeBid(999, "X");

            var result = repo.Update(999, updated);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_ReturnsTrueAndRemoves_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Bids.Add(MakeBid(7, "ToDelete"));
            ctx.SaveChanges();

            var repo = new BidListRepository(ctx);
            var result = repo.Delete(7);

            Assert.IsTrue(result);
            Assert.IsNull(ctx.Bids.Find(7));
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new BidListRepository(ctx);

            var result = repo.Delete(12345);

            Assert.IsFalse(result);
        }
    }
}