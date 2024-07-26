using LiqPay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentAPI.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static readonly string _publicKey = "";
        private static readonly string _privateKey = "";
        private static LiqPayCheckoutFormModel GetLiqPayModel(Order order)
        {
            var signatureSource = new LiqPayCheckout
            {
                PublicKey = _publicKey,
                Version = 3,
                Action = "pay",
                Amount = order.Amount,
                Currency = order.Currency,
                Description = order.Description,
                OrderId = order.OrderId,
                Sandbox = 1,
                ProductCategory = "Goods",
                ProductDescription = order.Description,
                ProductName = "Order Payment"
            };

            var jsonString = JsonConvert.SerializeObject(signatureSource);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            var signatureHash = GetLiqPaySignature(dataHash);

            return new LiqPayCheckoutFormModel
            {
                Data = dataHash,
                Signature = signatureHash
            };
        }
        private static string GetLiqPaySignature(string data)
        {
            var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(_privateKey + data + _privateKey));
            return Convert.ToBase64String(hash);
        }

        [HttpGet("generate-payment-url/{id}")]
        public IActionResult GeneratePaymentUrl(string id)
        {
            var order = new Order
            {
                OrderId = id,
                Amount = 100.00m,
                Currency = "UAH",
                Description = "Оплата заказа №" + id
            };

            var liqPayModel = GetLiqPayModel(order);
            var paymentUrl = $"https://www.liqpay.ua/api/3/checkout?data={Uri.EscapeDataString(liqPayModel.Data)}&signature={Uri.EscapeDataString(liqPayModel.Signature)}";

            return Ok(new LiqPayCheckoutUrlModel { Url = paymentUrl });
        }
    }
}
