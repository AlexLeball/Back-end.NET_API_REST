using P7CreateRestApi.Domain;


namespace P7CreateRestApi.Services.Interfaces
{
    public interface ITradeService
    {
        IEnumerable<Trade> GetAll();
        Trade? GetById(int id);
        void Add(Trade trade);
        bool Update(int id, Trade trade);
        bool Delete(int id);
    }
}
