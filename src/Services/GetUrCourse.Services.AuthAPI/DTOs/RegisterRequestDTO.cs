namespace GetUrCourse.Services.AuthAPI.DTOs
{
    public class RegisterRequestDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        // Remove in PROD!!!
        public string Role { get; set; }
    }
}
