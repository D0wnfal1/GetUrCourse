namespace PaymentAPI.Model
{
    public class PaymentModel
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CallbackUrl { get; set; }
    }
}
