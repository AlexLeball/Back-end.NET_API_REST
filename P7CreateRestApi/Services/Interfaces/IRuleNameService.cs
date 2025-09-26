using System.Collections.Generic;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRuleNameService
    {
        IEnumerable<RuleName> GetAll();
        RuleName? GetById(int id);
        void Add(RuleName ruleName);
        bool Update(int id, RuleName ruleName);
        bool Delete(int id);
    }
}
