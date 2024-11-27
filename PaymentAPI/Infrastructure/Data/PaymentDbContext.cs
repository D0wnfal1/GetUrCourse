using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace GetUrCourse.Services.PaymentAPI.Infrastructure.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
        public DbSet<Payment> Payments { get; set; }
    }
}
