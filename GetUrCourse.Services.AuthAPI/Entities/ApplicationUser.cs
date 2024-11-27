using Microsoft.AspNetCore.Identity;

namespace GetUrCourse.Services.AuthAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
