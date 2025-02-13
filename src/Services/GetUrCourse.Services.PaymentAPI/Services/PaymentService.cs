using GetUrCourse.Services.PaymentAPI.Constants;
using GetUrCourse.Services.PaymentAPI.Infrastructure.Repositories;
using Newtonsoft.Json;
using PaymentAPI.Model;
using System.Security.Cryptography;
using System.Text;

public class PaymentService
{
    private readonly string _publicKey;
    private readonly string _privateKey;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(string publicKey, string privateKey, IPaymentRepository paymentRepository)
    {
        _publicKey = publicKey;
        _privateKey = privateKey;
        _paymentRepository = paymentRepository;
    }
    /// <summary>
    /// Create URL for LiqPay payment.
    /// </summary>
    /// <returns>URL for LiqPay payment.</returns>
    public string CreatePayment(string orderId, string action, decimal amount, string description)
    {
        var data = new Dictionary<string, string>
    {
        // API LiqPay version
        {"version", PaymentSettings.ApiVersion.ToString()},
        {"public_key", _publicKey},
        // action of payment (pay, subscribe ... etc)
        {"action", action.ToLower()},
        {"amount", amount.ToString()},
        // payment currency (UAH, USD ...)
        {"currency", "UAH"},
        {"description", description},
        {"order_id", orderId},
            // result_url for check payment status
        {"result_url", $"https://localhost:7064/api/Payment/Redirect"}
    };

        if (action.ToLower() == "subscribe")
        {
            // Only for subscribe action ...
            data.Add("subscribe_date_start", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            data.Add("subscribe_periodicity", "month");
        }

        var json = JsonConvert.SerializeObject(data);
        var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        var signature = GenerateSignature(base64Data);

        var paymentUrl = $"{PaymentSettings.LiqpayApiCheckoutUrl}?data={base64Data}&signature={signature}";
        return paymentUrl;
    }
    private string GenerateSignature(string base64Data)
    {
        var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(_privateKey + base64Data + _privateKey));
        sha1.Dispose();
        return Convert.ToBase64String(hash);
    }
    public async Task<string> CreatePaymentAsync(string orderId, string action, decimal amount, string description)
    {
	    if (amount <= 0)
	    {
		    throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
	    }
		var paymentUrl = CreatePayment(orderId, action, amount, description);
        var payment = new Payment
        {
            OrderId = orderId,
            Action = action,
            Amount = amount,
            Description = description,
            Status = PaymentSettings.IsCreated,
            CreatedAt = DateTime.UtcNow
        };
        await _paymentRepository.AddAsync(payment);
        return paymentUrl;
    }
	/// <summary>
	/// Update Payment Result Status
	/// </summary>
	/// <returns>bool value</returns>
	public async Task<bool> HandlePaymentResultAsync(Dictionary<string, string> requestDictionary)
	{
		if (requestDictionary.TryGetValue("data", out var base64Data) &&
		    requestDictionary.TryGetValue("signature", out var signature))
		{
			var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
			var requestData = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedData);

			if (!requestData.TryGetValue("order_id", out var orderId))
			{
				return false;
			}

			var payment = await GetPaymentStatusAsync(orderId);
			if (payment == null)
			{
				return false;
			}

			if (requestData.TryGetValue("status", out var status) &&
			    (status == "success" || status == "subscribed"))
			{
				payment.Status = PaymentSettings.IsSuccess;

				if (payment.Action.Equals("subscribe", StringComparison.OrdinalIgnoreCase))
				{
					payment.SubscriptionEndDate = DateTime.UtcNow.AddDays(30);
				}
			}
			else
			{
				payment.Status = PaymentSettings.IsCanceled;
			}

			await SavePaymentAsync(payment);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Create LiqPay request with action "unsubscribe".
	/// </summary>
	/// <returns>bool value</returns>
	public async Task<bool> UnsubscribeAsync(string orderId)
    {
        var data = new Dictionary<string, string>
    {
        {"version", PaymentSettings.ApiVersion.ToString()},
        {"public_key", _publicKey},
        {"action", "unsubscribe"},
        {"order_id", orderId}
    };

        var json = JsonConvert.SerializeObject(data);
        var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        var signature = GenerateSignature(base64Data);

        var unsubscribeUrl = PaymentSettings.LiqpayApiUrl;

        using (var httpClient = new HttpClient())
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"data", base64Data},
            {"signature", signature}
        });

            var response = await httpClient.PostAsync(unsubscribeUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

                if (responseData["status"] == "unsubscribed")
                {
                    var payment = await GetPaymentStatusAsync(orderId);
                    payment.Status = PaymentSettings.IsUnsubscribed;
                    payment.SubscriptionEndDate = DateTime.UtcNow;
                    await SavePaymentAsync(payment);

                    return true;
                }
            }
        }
                                                                                                                                                                                                                                                                                                                                                                            
        return false;
    }


    public async Task<Payment> GetPaymentStatusAsync(string orderId)
    {
        return await _paymentRepository.GetByOrderIdAsync(orderId);
    }

    public async Task SavePaymentAsync(Payment payment)
    {
        await _paymentRepository.UpdateAsync(payment);
    }
}
