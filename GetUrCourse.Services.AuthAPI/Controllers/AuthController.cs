using GetUrCourse.Contracts.User;
using GetUrCourse.Services.AuthAPI.DTOs;
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
        private readonly IBus _publishEndpoint;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, IBus publishEndpoint, ILogger<AuthController> logger)
        {
            _authService = authService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>Returns a registered user or an error message.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO model)
        {
            _logger.LogInformation("Registering user: {UserName}", model.UserName);
            var (user, errorMessage) = await _authService.RegisterAsync(model);
            
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(errorMessage);
            }
            
            await _publishEndpoint.Publish(new AddUser(
                Guid.Parse(user.Id), 
                user.Email,
                user.Name,
                model.Role,
                DateTime.UtcNow));
            
            _logger.LogInformation("Registering user finished: {UserName}", model.UserName);
            
            return Ok();
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
