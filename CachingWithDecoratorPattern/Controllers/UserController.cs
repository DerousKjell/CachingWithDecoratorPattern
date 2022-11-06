using CachingWithDecoratorPattern.Domain;
using CachingWithDecoratorPattern.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CachingWithDecoratorPattern.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            var user = new User
            {
                Email = request.Email
            };

            await _userRepository.Add(user);

            return Ok(user);
        }
    }

    public class CreateUserRequest
    {
        public string Email { get; set; }
    }
}