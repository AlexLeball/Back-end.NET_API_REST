using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace P7CreateRestApi.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userRepository.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Add a new user with role and fullname
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegisterDto model)
        {
            var user = new User { UserName = model.Email, Email = model.Email, Fullname = model.Fullname };
            var result = await _userRepository.AddAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User created successfully");
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] RegisterDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
                return Unauthorized();

            var user = await _userRepository.FindByIdAsync(userId);
            if (user is null)
                return NotFound();

            user.Email = model.Email;
            user.UserName = model.Email;
            user.Fullname = model.Fullname;

            var result = await _userRepository.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
                return Unauthorized();
            
            var result = await _userRepository.DeleteAsync(userId);
            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok("User deleted successfully");
        }
    }

    //DTO for user registration 
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Fullname { get; set; }
    }
}