using System.ComponentModel.DataAnnotations;

namespace GetUrCourse.Services.AuthAPI.Models;

public class UserDto
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
}