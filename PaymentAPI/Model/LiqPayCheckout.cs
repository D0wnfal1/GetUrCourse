using System.ComponentModel.DataAnnotations;

namespace LiqPay.Models
{
    public class LiqPayCheckout
    {
        public int Version { get; set; }
        public string PublicKey { get; set; }
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string ProductCategory { get; set; }
        [StringLength(500)]
        public string ProductDescription { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        public int Sandbox { get; set; }
    }
}
