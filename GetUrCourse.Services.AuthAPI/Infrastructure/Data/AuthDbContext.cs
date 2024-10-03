using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.PaymentAPI.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
    }
}
