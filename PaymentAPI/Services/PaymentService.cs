using GetUrCourse.Services.PaymentAPI.Constants;
using GetUrCourse.Services.PaymentAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            // Only for subscribe action ...
            //{"subscribe_date_start", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")},
            //{"subscribe_periodicity", "month"}
            //{"resultUrl", $"api/PaymentController/Payment"}
        {"result_url", $"https://localhost:7064/api/Payment/Redirect"}
    };

        if (action.ToLower() == "subscribe")
        {
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
        var paymentUrl = CreatePayment(orderId, action, amount, description);
        var payment = new Payment
        {
            OrderId = orderId,
            Action = action,
            Amount = amount,
            Description = description,
            Status = "Created",
            CreatedAt = DateTime.UtcNow
        };
        await _paymentRepository.AddAsync(payment);
        return paymentUrl;
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
