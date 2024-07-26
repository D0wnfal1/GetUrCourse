using Newtonsoft.Json;

namespace PaymentAPI.Model
{
    public class LiqPayRequest
    {
        [JsonProperty("version")]
        public int Version { get; set; } = 3;

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; } = "pay";

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("server_url")]
        public string ServerUrl { get; set; }
    }
}
