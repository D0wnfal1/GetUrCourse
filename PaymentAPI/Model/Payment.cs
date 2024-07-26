using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Model
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
