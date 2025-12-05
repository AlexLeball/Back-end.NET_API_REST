using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        // POST: /trade
        [HttpPost]
        public IActionResult AddTrade([FromBody] Trade trade)
        {
            if (trade == null) return BadRequest();
            _tradeRepository.Add(trade);
            return Ok(trade);
        }

        // GET: /trade/{id}
        [HttpGet("{id}")]
        public IActionResult FindTrade(int id)
        {
            var trade = _tradeRepository.GetById(id);
            if (trade == null) return NotFound();
            return Ok(trade);
        }

        // PUT: /trade/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTrade(int id, [FromBody] Trade trade)
        {
            if (trade == null) return BadRequest();

            var updated = _tradeRepository.Update(id, trade);
            if (!updated) return NotFound();

            return Ok(_tradeRepository.GetById(id));
        }

        // DELETE: /trade/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTrade(int id)
        {
            var deleted = _tradeRepository.Delete(id);
            if (!deleted) return NotFound();

            return Ok(_tradeRepository.GetAll());
        }
    }
}