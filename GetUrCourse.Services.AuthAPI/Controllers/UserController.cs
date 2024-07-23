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

    public UserController(AppDbContext db, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpPost("register_student")]
    public async Task<IActionResult> Register([FromBody]UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Your data is not valid");
        }
        var existingUserLogin = await _userManager.FindByNameAsync(userDto.Login);
        var existingUserEmail = await _userManager.FindByEmailAsync(userDto.Email);
        
        if (existingUserLogin != null)
        {
            return BadRequest("This login is already taken");
        }
        if (existingUserEmail != null)
        {
            return BadRequest("This email is already taken");
        }

        IdentityUser user = new IdentityUser()
        {
            Email = userDto.Email,
            UserName = userDto.FirstName + "" + userDto.LastName,
            PasswordHash = userDto.Password.GetHashCode().ToString(),
            
        };
        
        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, WC.StudentRole);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("You created account");
        }

        return BadRequest();
    }
    
}