using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace PaymentAPI.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
            
        }
        public DbSet<Payment> Payments { get; set; }
    }
}
