using P7CreateRestApi.Domain;


namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        IEnumerable<Trade> GetAll();
        Trade? GetById(int id);
        void Add(Trade trade);
        bool Update(int id, Trade trade);
        bool Delete(int id);
    }
}
