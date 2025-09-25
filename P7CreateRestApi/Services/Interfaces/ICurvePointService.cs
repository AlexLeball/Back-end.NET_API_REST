using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface ICurvePointService
    {
        IEnumerable<CurvePoint> GetAll();
        CurvePoint? GetById(int id);
        void Add(CurvePoint curvePoint);
        bool Update(int id, CurvePoint curvePoint);
        bool Delete(int id);
    }
}