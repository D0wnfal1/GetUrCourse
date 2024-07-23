using GetUrCourse.Services.AuthAPI.Data;
using GetUrCourse.Services.AuthAPI.Models;
using GetUrCourse.Services.AuthAPI.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.AuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(AppDbContext db, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Register a new user with a specified role.
    /// </summary>
    /// <returns>Result of the registration</returns>
    [HttpPost("register_user")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Your data is not valid");
        }

        if (!IsValidRole(userDto.Role))
        {
            return BadRequest("Invalid role specified");
        }

        var existingUserEmail = await _userManager.FindByEmailAsync(userDto.Email);

        if (existingUserEmail != null)
        {
            return BadRequest("This email is already taken");
        }

        IdentityUser user = new IdentityUser()
        {
            Email = userDto.Email,
            UserName = userDto.Email
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(userDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
            }
            await _userManager.AddToRoleAsync(user, userDto.Role);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Account created successfully");
        }

        return BadRequest(result.Errors);
    }

    private bool IsValidRole(string role)
    {
        return role == WC.AdminRole || role == WC.StudentRole || role == WC.TeacherRole;
    }
}
