using GetUrCourse.Services.AuthAPI.DTOs;
using GetUrCourse.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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
