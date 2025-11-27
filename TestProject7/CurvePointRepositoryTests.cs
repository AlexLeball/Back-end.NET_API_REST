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
    public sealed class CurvePointRepositoryTests
    {
        private static LocalDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new LocalDbContext(options);
        }

        private static CurvePoint MakeCurvePoint(int id = 0, byte curveId = 1)
        {
            return new CurvePoint
            {
                Id = id,
                CurveId = curveId,
                AsOfDate = DateTime.Today,
                Term = 1.5,
                CurvePointValue = 2.5,
                CreationDate = DateTime.Now
            };
        }

        [TestMethod]
        public void GetAll_ReturnsAllCurvePoints()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.CurvePoints.Add(MakeCurvePoint(1));
            ctx.CurvePoints.Add(MakeCurvePoint(2));
            ctx.SaveChanges();

            var repo = new CurvePointRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(c => c.Id == 1));
            Assert.IsTrue(all.Any(c => c.Id == 2));
        }

        [TestMethod]
        public void GetById_ReturnsCurvePoint_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.CurvePoints.Add(MakeCurvePoint(42));
            ctx.SaveChanges();

            var repo = new CurvePointRepository(ctx);
            var found = repo.GetById(42);

            Assert.IsNotNull(found);
            Assert.AreEqual(42, found.Id);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new CurvePointRepository(ctx);

            var found = repo.GetById(999);

            Assert.IsNull(found);
        }

        [TestMethod]
        public void Add_AddsCurvePointToDatabase()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new CurvePointRepository(ctx);

            var curvePoint = MakeCurvePoint();
            repo.Add(curvePoint);

            var all = ctx.CurvePoints.ToList();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual(curvePoint.CurveId, all[0].CurveId);
        }

        [TestMethod]
        public void Update_ReturnsTrueAndUpdates_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.CurvePoints.Add(MakeCurvePoint(5, 10));
            ctx.SaveChanges();

            var repo = new CurvePointRepository(ctx);
            var updated = MakeCurvePoint(5, 20);
            var result = repo.Update(5, updated);

            Assert.IsTrue(result);
            var persisted = ctx.CurvePoints.Find(5);
            Assert.IsNotNull(persisted);
            Assert.AreEqual((byte)20, persisted.CurveId);
        }

        [TestMethod]
        public void Update_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new CurvePointRepository(ctx);

            var result = repo.Update(999, MakeCurvePoint(999));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_ReturnsTrueAndRemoves_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.CurvePoints.Add(MakeCurvePoint(7));
            ctx.SaveChanges();

            var repo = new CurvePointRepository(ctx);
            var result = repo.Delete(7);

            Assert.IsTrue(result);
            Assert.IsNull(ctx.CurvePoints.Find(7));
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new CurvePointRepository(ctx);

            var result = repo.Delete(12345);

            Assert.IsFalse(result);
        }
    }
}