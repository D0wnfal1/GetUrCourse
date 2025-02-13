using GetUrCourse.Services.PaymentAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace GetUrCourse.Services.PaymentAPI.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetByOrderIdAsync(string orderId)
        {
            return await _context.Payments.SingleOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
