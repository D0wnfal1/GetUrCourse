using LiqPay.SDK.Dto;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Data;
using PaymentAPI.Model;
using System;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly LiqPayService _liqPayService;
        private readonly PaymentContext _context;

        public PaymentController(LiqPayService liqPayService, PaymentContext context)
        {
            _liqPayService = liqPayService;
            _context = context;
        }

        [HttpPost]
        [Route("pay")]
        public async Task<IActionResult> PayAsync([FromBody] LiqPay.SDK.Dto.LiqPayRequest liqPayRequest)
        {
            LiqPay.SDK.Dto.LiqPayResponse response = await _liqPayService.MakePaymentAsync(liqPayRequest);

                var payment = new Payment
                {
                    OrderId = response.OrderId,
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return Ok(response);
        }
    }
}
