using Newtonsoft.Json;

namespace GetUrCourse.Services.PaymentAPI.Models
{
    public class PaymentRequestDTO
    {

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Action type for payment. Possible values: "pay", "subscribe", "unsubscribe".
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
