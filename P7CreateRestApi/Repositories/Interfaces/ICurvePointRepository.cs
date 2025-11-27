using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface ICurvePointRepository
    {
        IEnumerable<CurvePoint> GetAll();
        CurvePoint? GetById(int id);
        void Add(CurvePoint curvePoint);
        bool Update(int id, CurvePoint curvePoint);
        bool Delete(int id);
    }
}