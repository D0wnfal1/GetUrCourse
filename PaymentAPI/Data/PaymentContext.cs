using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
            
        }
    }
}
