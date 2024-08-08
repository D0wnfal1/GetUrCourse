using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Model;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly LiqPayService _liqPayService;

    public PaymentController(LiqPayService liqPayService)
    {
        _liqPayService = liqPayService;
    }
    /// <summary>
    /// Create Url for payment n return itself.
    /// </summary>
    /// <param name="request">Payment request model.</param>
    /// <returns>Url for payment</returns>
    [HttpPost("CreatePayment")]
    public IActionResult CreatePayment([FromBody] PaymentModel request)
    {
        var paymentUrl = _liqPayService.CreatePayment(request.OrderId, request.Amount, request.Description);
        return Ok(new { url = paymentUrl });
    }
}