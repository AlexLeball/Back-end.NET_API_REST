using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNameController(IRuleNameRepository ruleNameService)
        {
            _ruleNameRepository = ruleNameService;
        }

        [HttpPost]
        public IActionResult AddRuleName([FromBody] RuleName ruleName)
        {
            if (ruleName == null) return BadRequest("Invalid RuleName data");

            _ruleNameRepository.Add(ruleName);
            return Ok(ruleName);
        }

        [HttpGet("{id}")]
        public IActionResult GetRuleName(int id)
        {
            var ruleName = _ruleNameRepository.GetById(id);
            if (ruleName == null) return NotFound(); 

            return Ok(ruleName);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRuleName(int id, [FromBody] RuleName ruleName)
        {
            if (ruleName == null) return BadRequest("Invalid RuleName data");

            var updated = _ruleNameRepository.Update(id, ruleName);
            if (!updated) return NotFound($"RuleName with id {id} not found");

            return Ok(_ruleNameRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRuleName(int id)
        {
            var deleted = _ruleNameRepository.Delete(id);
            if (!deleted) return NotFound($"RuleName with id {id} not found");

            return Ok(_ruleNameRepository.GetAll());
        }

    }
}
