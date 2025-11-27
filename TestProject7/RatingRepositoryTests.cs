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
    public sealed class RatingRepositoryTests
    {
        private static LocalDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(dbName) // Requires Microsoft.EntityFrameworkCore.InMemory
                .Options;
            return new LocalDbContext(options);
        }

        private static Rating MakeRating(int id = 0, string moodys = "Aaa", string sp = "AAA", string fitch = "AAA", byte order = 1)
        {
            return new Rating
            {
                Id = id,
                MoodysRating = moodys,
                SandPRating = sp,
                FitchRating = fitch,
                OrderNumber = order
            };
        }

        [TestMethod]
        public void GetAll_ReturnsAllRatings()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Ratings.Add(MakeRating(1));
            ctx.Ratings.Add(MakeRating(2));
            ctx.SaveChanges();

            var repo = new RatingRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(r => r.Id == 1));
            Assert.IsTrue(all.Any(r => r.Id == 2));
        }

        [TestMethod]
        public void GetById_ReturnsRating_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Ratings.Add(MakeRating(42, "Aa", "AA", "AA"));
            ctx.SaveChanges();

            var repo = new RatingRepository(ctx);
            var found = repo.GetById(42);

            Assert.IsNotNull(found);
            Assert.AreEqual("Aa", found.MoodysRating);
        }

        [TestMethod]
        public void Add_AddsRatingToDatabase()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RatingRepository(ctx);

            var rating = MakeRating();
            repo.Add(rating);

            var all = ctx.Ratings.ToList();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual(rating.MoodysRating, all[0].MoodysRating);
        }

        [TestMethod]
        public void Update_ReturnsTrueAndUpdates_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Ratings.Add(MakeRating(5, "Before", "B", "B"));
            ctx.SaveChanges();

            var repo = new RatingRepository(ctx);
            var updated = MakeRating(5, "After", "A", "A");
            var result = repo.Update(5, updated);

            Assert.IsTrue(result);
            var persisted = ctx.Ratings.Find(5);
            Assert.AreEqual("After", persisted.MoodysRating);
        }

        [TestMethod]
        public void Update_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RatingRepository(ctx);

            var result = repo.Update(999, MakeRating(999));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_ReturnsTrueAndRemoves_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.Ratings.Add(MakeRating(7));
            ctx.SaveChanges();

            var repo = new RatingRepository(ctx);
            var result = repo.Delete(7);

            Assert.IsTrue(result);
            Assert.IsNull(ctx.Ratings.Find(7));
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RatingRepository(ctx);

            var result = repo.Delete(12345);
            Assert.IsFalse(result);
        }
    }
}
