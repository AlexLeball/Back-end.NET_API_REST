using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories.Interfaces;

// Ajoutez cette directive using si l'interface ICurvePointService se trouve dans un autre namespace
// using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurveController(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        // GET: api/curve
        [HttpGet]
        public IActionResult GetAllCurvePoints()
        {
            var curvePoints = _curvePointRepository.GetAll();
            return Ok(curvePoints);
        }

  
        [HttpPost]
        public IActionResult AddCurvePoint([FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _curvePointRepository.Add(curvePoint);
            return Ok(_curvePointRepository.GetAll());
        }


        // GET: api/curve/5
        [HttpGet("{id}")]
        public IActionResult GetCurvePoint(int id)
        {
            var curvePoint = _curvePointRepository.GetById(id);
            if (curvePoint == null)
                return NotFound();

            return Ok(curvePoint);
        }

        // PUT: api/curve/5
        [HttpPut("{id}")]
        public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _curvePointRepository.Update(id, curvePoint);
            if (!updated)
                return NotFound();

            return Ok(_curvePointRepository.GetAll());
        }

        // DELETE: api/curve/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCurvePoint(int id)
        {
            var deleted = _curvePointRepository.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(_curvePointRepository.GetAll());
        }
    }
}
