using GetUrCourse.Services.AuthAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> GetAuthentication()
        {
            var userRoles = User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value);

            return $"You are authenticated with roles: {string.Join(", ", userRoles)}";
        }

        [HttpGet("admin")]
        [Authorize(Roles = AuthSettings.Role_Admin)]
        public async Task<ActionResult<string>> GetAuthorizetion()
        {
            return "You are Authorized with Role of Admin";
        }
    }
}
