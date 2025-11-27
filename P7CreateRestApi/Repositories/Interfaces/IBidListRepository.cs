using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IBidListRepository
    {
        IEnumerable<BidList> GetAll();
        BidList? GetById(int id);
        void Add(BidList bid);
        bool Update(int id, BidList bid);
        bool Delete(int id);
 
    }
}
