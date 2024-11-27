using GetUrCourse.Services.PaymentAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        var paymentUrl = _paymentService.CreatePaymentAsync(request.OrderId, request.Action, request.Amount, request.Description);
        return Ok(new { url = paymentUrl });
    }
    [HttpPost("Unsubscribe")]
    public async Task<IActionResult> Unsubscribe([FromForm] string orderId)
    {
        bool isUnsubscribed = await _paymentService.UnsubscribeAsync(orderId);

        if (isUnsubscribed)
        {
            return Ok(new { message = "Subscription cancelled successfully." });
        }
        else
        {
            return BadRequest(new { message = "Failed to cancel subscription." });
        }
    }
    [HttpPost("Redirect")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Redirect()
    {
        var requestDictionary = Request.Form.ToDictionary(key => key.Key, key => key.Value.ToString());

        bool isSuccess = await _paymentService.HandlePaymentResultAsync(requestDictionary);
        if (isSuccess)
        {
            return Redirect("https://localhost:7064/swagger/index.html");
        }
        else
        {
            return BadRequest(new { message = "Failed to update payment status" });
        }
    }
}