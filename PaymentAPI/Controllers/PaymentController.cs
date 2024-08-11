using GetUrCourse.Services.PaymentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Model;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("CreatePayment")]
    public async Task<IActionResult> CreatePayment([FromForm] PaymentRequestDTO request)
    {
        var paymentUrl = await _paymentService.CreatePaymentAsync(request.OrderId, request.Action, request.Amount, request.Description);
        return Ok(new { url = paymentUrl });
    }

    [HttpGet("GetPaymentStatus/{orderId}")]
    public async Task<IActionResult> GetPaymentStatus(string orderId)
    {
        var payment = await _paymentService.GetPaymentStatusAsync(orderId);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }
}