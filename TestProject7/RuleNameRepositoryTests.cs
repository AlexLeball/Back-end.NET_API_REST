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
    public sealed class RuleNameRepositoryTests
    {
        private static LocalDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(dbName) // Requires Microsoft.EntityFrameworkCore.InMemory
                .Options;
            return new LocalDbContext(options);
        }

        private static RuleName MakeRuleName(int id = 0, string name = "Rule", string description = "Desc")
        {
            return new RuleName
            {
                Id = id,
                Name = name,
                Description = description,
                Json = "{}",
                Template = "Template",
                SqlStr = "SELECT 1",
                SqlPart = "WHERE 1=1"
            };
        }

        [TestMethod]
        public void GetAll_ReturnsAllRuleNames()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.RuleNames.Add(MakeRuleName(1));
            ctx.RuleNames.Add(MakeRuleName(2));
            ctx.SaveChanges();

            var repo = new RuleNameRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(r => r.Id == 1));
            Assert.IsTrue(all.Any(r => r.Id == 2));
        }

        [TestMethod]
        public void GetById_ReturnsRuleName_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.RuleNames.Add(MakeRuleName(42, "TestRule"));
            ctx.SaveChanges();

            var repo = new RuleNameRepository(ctx);
            var found = repo.GetById(42);

            Assert.IsNotNull(found);
            Assert.AreEqual("TestRule", found.Name);
        }

        [TestMethod]
        public void Add_AddsRuleNameToDatabase()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RuleNameRepository(ctx);

            var rule = MakeRuleName();
            repo.Add(rule);

            var all = ctx.RuleNames.ToList();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual(rule.Name, all[0].Name);
        }

        [TestMethod]
        public void Update_ReturnsTrueAndUpdates_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.RuleNames.Add(MakeRuleName(5, "Before"));
            ctx.SaveChanges();

            var repo = new RuleNameRepository(ctx);
            var updated = MakeRuleName(5, "After");
            var result = repo.Update(5, updated);

            Assert.IsTrue(result);
            var persisted = ctx.RuleNames.Find(5);
            Assert.AreEqual("After", persisted.Name);
        }

        [TestMethod]
        public void Update_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RuleNameRepository(ctx);

            var result = repo.Update(999, MakeRuleName(999));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Delete_ReturnsTrueAndRemoves_WhenExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            ctx.RuleNames.Add(MakeRuleName(7));
            ctx.SaveChanges();

            var repo = new RuleNameRepository(ctx);
            var result = repo.Delete(7);

            Assert.IsTrue(result);
            Assert.IsNull(ctx.RuleNames.Find(7));
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new RuleNameRepository(ctx);

            var result = repo.Delete(12345);
            Assert.IsFalse(result);
        }
    }
}
