using Azure.Core;
using GetUrCourse.Services.PaymentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentAPI.Model;
using System.Text;
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
        var paymentUrl = _paymentService.CreatePaymentAsync(request.OrderId, request.Action, request.Amount, request.Description);
        return Ok(new { url = paymentUrl });
    }
    [HttpPost("Redirect")]
    public IActionResult Redirect()
    {
        var request_dictionary = Request.Form.Keys.ToDictionary(key => key, key => Request.Form[key]);
        byte[] request_data = Convert.FromBase64String(request_dictionary["data"]);
        string decodedString = Encoding.UTF8.GetString(request_data);
        var request_data_dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);

        if (request_data_dictionary["status"] == "success")
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}