using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;

        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
        }

        // POST: api/bidlist/validate
        [HttpPost("validate")]
        public IActionResult Validate([FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _bidListService.Add(bidList);
            return Ok(_bidListService.GetAll());
        }

        // GET: api/bidlist/update/5
        [HttpGet("update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            var bid = _bidListService.GetById(id);
            if (bid == null)
                return NotFound();

            return Ok(bid);
        }

        // PATCH: api/bidlist/update/5
        [HttpPatch("update/{id}")]
        public IActionResult UpdateBid(int id, [FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _bidListService.Update(id, bidList);
            if (!updated)
                return NotFound();

            return Ok(_bidListService.GetAll());
        }

        // DELETE: api/bidlist/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBid(int id)
        {
            var deleted = _bidListService.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(_bidListService);
        } 
    } 
}
