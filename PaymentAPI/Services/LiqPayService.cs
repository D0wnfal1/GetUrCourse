using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class LiqPayService
{
    private readonly string _publicKey;
    private readonly string _privateKey;
    private readonly HttpClient _httpClient;

    public LiqPayService(string publicKey, string privateKey)
    {
        _publicKey = publicKey;
        _privateKey = privateKey;
        _httpClient = new HttpClient();
    }
    /// <summary>
    /// Create URL for LiqPay payment.
    /// </summary>
    /// <returns>URL for LiqPay payment.</returns>
    public string CreatePayment(string orderId, string amount, string description)
    {
        var data = new Dictionary<string, string>
    {
        // API LiqPay version
        {"version", "3"},
        {"public_key", _publicKey},
        // action of payment (pay, subscribe ... etc)
        {"action", "subscribe"},
        {"amount", amount},
        // payment currency (UAH, USD ...)
        {"currency", "UAH"},
        {"description", description},
        {"order_id", orderId},
        // Only for subscribe action ...
        {"subscribe_date_start", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")},
        {"subscribe_periodicity", ("month")}
    };

        var json = JsonConvert.SerializeObject(data);
        var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        var signature = GenerateSignature(base64Data);

        var paymentUrl = $"https://www.liqpay.ua/api/3/checkout?data={base64Data}&signature={signature}";
        return paymentUrl;
    }


    private string GenerateSignature(string base64Data)
    {
        using (var sha1 = System.Security.Cryptography.SHA1.Create())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(_privateKey + base64Data + _privateKey));
            return Convert.ToBase64String(hash);
        }
    }
}
