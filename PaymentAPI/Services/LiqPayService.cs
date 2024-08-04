using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    public string CreatePayment(string orderId, decimal amount, string currency, string description, string callbackUrl)
    {
        var data = new Dictionary<string, string>
    {
        {"version", "3"},
        {"public_key", _publicKey},
        {"action", "pay"},
        {"amount", amount.ToString("F2")},
        {"currency", currency},
        {"description", description},
        {"order_id", orderId},
        {"server_url", callbackUrl}
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
