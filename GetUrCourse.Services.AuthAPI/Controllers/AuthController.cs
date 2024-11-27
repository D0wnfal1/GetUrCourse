using GetUrCourse.Services.AuthAPI.DTOs;
using GetUrCourse.Services.AuthAPI.ProducerMessage;
using GetUrCourse.Services.AuthAPI.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ITopicProducer<UserRegistrationMessage> _producer;

        public AuthController(AuthService authService, ITopicProducer<UserRegistrationMessage> producer)
        {
            _authService = authService;
            _producer = producer;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>Returns a registered user or an error message.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO model)
        {
            var (user, errorMessage) = await _authService.RegisterAsync(model);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(errorMessage);
            }

            UserRegistrationMessage message = new UserRegistrationMessage()
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                Name = user.Name
            };

            await _producer.Produce(message);

            return Ok(user);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model">The login details of the user.</param>
        /// <returns>Returns the user token if login is successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDTO model)
        {
            var (loginResponse, errorMessage) = await _authService.LoginAsync(model);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(errorMessage);
            }

            return Ok(loginResponse);
        }
    }
}
