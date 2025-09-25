using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;

// Ajoutez cette directive using si l'interface ICurvePointService se trouve dans un autre namespace
// using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurveController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        // GET: api/curve/list
        [HttpGet("list")]
        public IActionResult GetAllCurvePoints()
        {
            var curvePoints = _curvePointService.GetAll();
            return Ok(curvePoints);
        }

        // POST: api/curve/add
        [HttpPost("add")]
        public IActionResult AddCurvePoint([FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _curvePointService.Add(curvePoint);
            return Ok(_curvePointService.GetAll());
        }

        // POST: api/curve/validate
        [HttpPost("validate")]
        public IActionResult Validate([FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _curvePointService.Add(curvePoint); // Assuming validation happens inside service
            return Ok(_curvePointService.GetAll());
        }

        // GET: api/curve/update/5
        [HttpGet("update/{id}")]
        public IActionResult GetCurvePoint(int id)
        {
            var curvePoint = _curvePointService.GetById(id);
            if (curvePoint == null)
                return NotFound();

            return Ok(curvePoint);
        }

        // PUT: api/curve/update/5
        [HttpPut("update/{id}")]
        public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _curvePointService.Update(id, curvePoint);
            if (!updated)
                return NotFound();

            return Ok(_curvePointService.GetAll());
        }

        // DELETE: api/curve/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCurvePoint(int id)
        {
            var deleted = _curvePointService.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(_curvePointService.GetAll());
        }
    }
}
