namespace LiqPay.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
