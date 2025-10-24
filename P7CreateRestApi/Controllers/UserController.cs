using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]User user)
        {
            if (user == null)
                return BadRequest("Invalid user data");
            _userRepository.Add(user);
            return Ok(user);
        }

        [HttpGet ("{id}")]
        public IActionResult FindUser(int id)
        {
            User user = _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data");
            User existingUser = _userRepository.FindById(id);
            if (existingUser == null)
                throw new ArgumentException("Invalid user Id:" + id);
            existingUser.UserName = user.UserName;
            existingUser.Fullname = user.Fullname;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }
    }
}