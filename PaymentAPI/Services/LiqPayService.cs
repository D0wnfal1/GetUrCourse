using LiqPay.SDK.Dto;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

public class LiqPayService
{
    private readonly string _publicKey;
    private readonly string _privateKey;
    private readonly HttpClient _httpClient;

    public LiqPayService(string publicKey, string privateKey, HttpClient httpClient)
    {
        _publicKey = publicKey;
        _privateKey = privateKey;
        _httpClient = httpClient;
    }

    public async Task<LiqPayResponse> MakePaymentAsync(LiqPayRequest request)
    {
        try
        {
            request.PublicKey = _publicKey;

            string jsonRequest = JsonConvert.SerializeObject(request);

            string signature = GenerateSignature(jsonRequest);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("data", jsonRequest),
                new KeyValuePair<string, string>("signature", signature)
            });

            HttpResponseMessage response = await _httpClient.PostAsync("https://www.liqpay.ua/api/request", content);

            response.EnsureSuccessStatusCode();
            
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LiqPayResponse>(jsonResponse);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    private string GenerateSignature(string data)
    {
        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        string signatureString = _privateKey + base64 + _privateKey;
        using (SHA1 sha = new SHA1Managed())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(signatureString));
            return Convert.ToBase64String(hash);
        }
    }
}
