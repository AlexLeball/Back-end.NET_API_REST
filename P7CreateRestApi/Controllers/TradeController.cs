using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // GET: /trade/list
        [HttpGet("list")]
        public IActionResult Home()
        {
            var trades = _tradeService.GetAll();
            return Ok(trades);
        }

        // POST: /trade/add
        [HttpPost("add")]
        public IActionResult AddTrade([FromBody] Trade trade)
        {
            if (trade == null) return BadRequest("Invalid trade data");
            _tradeService.Add(trade);
            return Ok(trade);
        }

        // POST: /trade/validate
        [HttpPost("validate")]
        public IActionResult Validate([FromBody] Trade trade)
        {
            if (trade == null) return BadRequest("Invalid trade data");

            // Example validation: require Account and Trader
            if (string.IsNullOrWhiteSpace(trade.Account) || string.IsNullOrWhiteSpace(trade.Trader))
                return BadRequest("Account and Trader are required");

            _tradeService.Add(trade);
            return Ok(_tradeService.GetAll());
        }

        // GET: /trade/update/{id}
        [HttpGet("update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            var trade = _tradeService.GetById(id);
            if (trade == null) return NotFound($"Trade with id {id} not found");
            return Ok(trade);
        }

        // PUT: /trade/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult UpdateTrade(int id, [FromBody] Trade trade)
        {
            if (trade == null) return BadRequest("Invalid trade data");

            var updated = _tradeService.Update(id, trade);
            if (!updated) return NotFound($"Trade with id {id} not found");

            return Ok(_tradeService.GetAll());
        }

        // DELETE: /trade/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTrade(int id)
        {
            var deleted = _tradeService.Delete(id);
            if (!deleted) return NotFound($"Trade with id {id} not found");

            return Ok(_tradeService.GetAll());
        }
    }
}