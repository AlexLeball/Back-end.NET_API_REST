using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IBidListService
    {
        IEnumerable<BidList> GetAll();
        BidList? GetById(int id);
        void Add(BidList bid);
        bool Update(int id, BidList bid);
        bool Delete(int id);
 
    }
}
