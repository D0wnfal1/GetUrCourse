using PaymentAPI.Model;

namespace GetUrCourse.Services.PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(string orderId, string action, decimal amount, string description);
        Task<Payment> GetPaymentStatusAsync(string orderId);
        Task SavePaymentAsync(Payment payment);
    }
}
