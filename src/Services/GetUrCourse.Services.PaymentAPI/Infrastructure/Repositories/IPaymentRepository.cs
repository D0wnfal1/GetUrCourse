using PaymentAPI.Model;

namespace GetUrCourse.Services.PaymentAPI.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetByOrderIdAsync(string orderId);
		Task UpdateAsync(Payment payment);
    }
}
