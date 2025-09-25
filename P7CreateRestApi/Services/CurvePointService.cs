using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        public LocalDbContext DbContext { get; }

        public CurvePointService(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<CurvePoint> GetAll()
        {
            return DbContext.CurvePoints.ToList();
        }

        public CurvePoint? GetById(int id)
        {
            return DbContext.CurvePoints.FirstOrDefault(c => c.Id == id);
        }

        public void Add(CurvePoint curvePoint)
        {
            DbContext.CurvePoints.Add(curvePoint);
            DbContext.SaveChanges();
        }

        public bool Update(int id, CurvePoint curvePoint)
        {
            var existing = DbContext.CurvePoints.Find(id);
            if (existing == null) return false;
            existing.CurveId = curvePoint.CurveId;
            existing.AsOfDate = curvePoint.AsOfDate;
            existing.Term = curvePoint.Term;
            existing.CurvePointValue = curvePoint.CurvePointValue;
            existing.CreationDate = curvePoint.CreationDate;
            DbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var curvePoint = DbContext.CurvePoints.Find(id);
            if (curvePoint == null) return false;
            DbContext.CurvePoints.Remove(curvePoint);
            DbContext.SaveChanges();
            return true;
        }
    }
}
