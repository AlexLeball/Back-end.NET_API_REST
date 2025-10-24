using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingService;

        public RatingController(IRatingRepository ratingService)
        {
            _ratingService = ratingService;
        }

        // GET: api/rating
        [HttpGet]
        public IActionResult GetAll()
        {
            var ratings = _ratingService.GetAll();
            return Ok(ratings);
        }

        // POST: api/rating
        [HttpPost]
        public IActionResult Add([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _ratingService.Add(rating);
            return Ok(_ratingService.GetAll());
        }


        // GET: api/rating/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rating = _ratingService.GetById(id);
            if (rating == null)
                return NotFound();

            return Ok(rating);
        }

        // PUT: api/rating/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRating(int id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _ratingService.Update(id, rating);
            if (!updated)
                return NotFound();

            return Ok(_ratingService.GetAll());
        }

        // DELETE: api/rating/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            var deleted = _ratingService.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(_ratingService.GetAll());
        }
    }
}