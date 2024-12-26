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

        public AuthController(AuthService authService, IBus publishEndpoint)
        {
            _authService = authService;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>Returns a registered user or an error message.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO model)
        {
            // var (user, errorMessage) = await _authService.RegisterAsync(model);
            //
            // if (!string.IsNullOrEmpty(errorMessage))
            // {
            //     return BadRequest(errorMessage);
            // }
            
            await _publishEndpoint.Publish(new AddUser(
                Guid.NewGuid(),
                model.UserName,
                model.Name,
                model.Role,
                DateTime.UtcNow));
            Console.WriteLine("User add command published");
            
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
