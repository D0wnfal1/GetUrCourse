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

    [HttpPost("CreatePayment")]
    public IActionResult CreatePayment([FromBody] PaymentModel request)
    {
        var paymentUrl = _liqPayService.CreatePayment(request.OrderId, request.Amount, request.Currency, request.Description, request.CallbackUrl);
        return Ok(new { url = paymentUrl });
    }
}