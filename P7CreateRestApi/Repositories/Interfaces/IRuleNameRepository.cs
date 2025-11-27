using System.Collections.Generic;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRuleNameRepository
    {
        IEnumerable<RuleName> GetAll();
        RuleName? GetById(int id);
        void Add(RuleName ruleName);
        bool Update(int id, RuleName ruleName);
        bool Delete(int id);
    }
}
