using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
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

        [HttpGet]
        [Route("{id}")]
        public IActionResult FindUser(int id)
        {
            User user = _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }

        [HttpPost]
        [Route("{id}")]
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
            return Ok(_userRepository.FindAll());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }
    }
}