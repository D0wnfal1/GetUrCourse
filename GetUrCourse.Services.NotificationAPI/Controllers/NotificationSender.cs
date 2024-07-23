using GetUrCourse.Services.NotificationAPI.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.NotificationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationSender : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public NotificationSender(IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _emailSender = emailSender;
    }

    [HttpPost("send_email")]
    public async Task<IActionResult> EmailSend(string email)
    {
        var pathToTemplate = Path.Combine(_webHostEnvironment.ContentRootPath, "Template", "Inquiry.html");

        // Проверка существования файла
        if (!System.IO.File.Exists(pathToTemplate))
        {
            return NotFound("Template file not found.");
        }

        string htmlBody;
        string subject = "New Inquiry";
        
        using (StreamReader sr = System.IO.File.OpenText(pathToTemplate))
        {
            htmlBody = await sr.ReadToEndAsync();
        }
        
        htmlBody = htmlBody.Replace("{0}", "productUserVm.FullName")
            .Replace("{1}", "productUserVm.User.Email")
            .Replace("{2}", "productUserVm.PhoneNumber")
            .Replace("{3}", "productListSb.ToString()")
            .Replace("{4}", 400.ToString());

        await _emailSender.SendEmailAsync(email, subject, htmlBody);
        await _emailSender.SendEmailAsync(WC.AdminEmail, subject, htmlBody);
        
        return Ok();
    }
}