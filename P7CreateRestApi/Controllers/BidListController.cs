using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListRepository _bidListRepository;

        public BidListController(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        // GET: api/bidlist/update/5
        [HttpGet("{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            var bid = _bidListRepository.GetById(id);
            if (bid == null)
                return NotFound();

            return Ok(bid);
        }

        // PATCH: api/bidlist/update/5
        [HttpPatch("{id}")]
        public IActionResult UpdateBid(int id, [FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _bidListRepository.Update(id, bidList);
            if (!updated)
                return NotFound();

            return Ok(_bidListRepository.GetAll());
        }

        // DELETE: api/bidlist/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBid(int id)
        {
            var deleted = _bidListRepository.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(_bidListRepository);
        } 
    } 
}
