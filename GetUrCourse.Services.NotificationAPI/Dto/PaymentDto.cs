namespace GetUrCourse.Services.NotificationAPI.Dto;

public class PaymentDto
{
    public Guid TransactionId { get; set; }
    public string AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
}