using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private LocalDbContext DbContext { get; }

        public RuleNameService(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<RuleName> GetAll()
        {
            return DbContext.RuleNames.ToList();
        }

        public RuleName? GetById(int id)
        {
            return DbContext.RuleNames.FirstOrDefault(r => r.Id == id);
        }

        public void Add(RuleName ruleName)
        {
            DbContext.RuleNames.Add(ruleName);
            DbContext.SaveChanges();
        }

        public bool Update(int id, RuleName ruleName)
        {
            var existingRuleName = DbContext.RuleNames.Find(id);
            if (existingRuleName == null) return false;

            existingRuleName.Name = ruleName.Name;
            existingRuleName.Description = ruleName.Description;
            existingRuleName.Json = ruleName.Json;
            existingRuleName.Template = ruleName.Template;
            existingRuleName.SqlStr = ruleName.SqlStr;
            existingRuleName.SqlPart = ruleName.SqlPart;

            DbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var ruleName = DbContext.RuleNames.Find(id);
            if (ruleName == null) return false;

            DbContext.RuleNames.Remove(ruleName);
            DbContext.SaveChanges();
            return true;
        }
    }
}
