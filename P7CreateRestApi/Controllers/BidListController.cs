using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListRepository _bidListRepository;

        public BidListController(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        [HttpPost]
        public IActionResult CreateBid([FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _bidListRepository.Add(bidList);
            return Ok(bidList);
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
        [HttpPut("{id}")]
        public IActionResult UpdateBid(int id, [FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _bidListRepository.Update(id, bidList);
            if (!updated)
                return NotFound();

            return Ok(_bidListRepository.GetById(id));
        }

        // DELETE: api/bidlist/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBid(int id)
        {
            var deleted = _bidListRepository.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok("bid deleted");
        } 
    } 
}
