using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameService _ruleNameService;

        public RuleNameController(IRuleNameService ruleNameService)
        {
            _ruleNameService = ruleNameService;
        }

        [HttpGet("list")]
        public IActionResult Home()
        {
            var ruleNames = _ruleNameService.GetAll();
            return Ok(ruleNames);
        }

        [HttpPost("add")]
        public IActionResult AddRuleName([FromBody] RuleName ruleName)
        {
            if (ruleName == null) return BadRequest("Invalid RuleName data");

            _ruleNameService.Add(ruleName);
            return Ok(ruleName);
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromBody] RuleName ruleName)
        {
            if (ruleName == null) return BadRequest("Invalid RuleName data");

            if (string.IsNullOrWhiteSpace(ruleName.Name))
                return BadRequest("Name is required");

            _ruleNameService.Add(ruleName);
            return Ok(_ruleNameService.GetAll());
        }

        [HttpGet("update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            var ruleName = _ruleNameService.GetById(id);
            if (ruleName == null) return NotFound($"RuleName with id {id} not found");

            return Ok(ruleName);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateRuleName(int id, [FromBody] RuleName ruleName)
        {
            if (ruleName == null) return BadRequest("Invalid RuleName data");

            var updated = _ruleNameService.Update(id, ruleName);
            if (!updated) return NotFound($"RuleName with id {id} not found");

            return Ok(_ruleNameService.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRuleName(int id)
        {
            var deleted = _ruleNameService.Delete(id);
            if (!deleted) return NotFound($"RuleName with id {id} not found");

            return Ok(_ruleNameService.GetAll());
        }

    }
}
