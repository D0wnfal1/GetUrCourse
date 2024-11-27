using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.TemplateReader;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;

public class NotificationService(IEmailSender emailSender, ITemplateReader templateReader) : INotificationService
{
    public async Task<bool> SendConfirmEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/Confirmation.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber)
            .Replace("{3}", "");

        await emailSender.SendEmailAsync(userDto.Email, Wc.ConfirmEmail, htmlBody);
        return true;
    }
    public async Task<bool> SendRegisterCourseEmailAsync(UserDto userDto, string courseName)
    {
        var templatePath = "Template/CourseRegister.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber)
            .Replace("{3}", courseName);

        await emailSender.SendEmailAsync(userDto.Email, Wc.CourseRegister, htmlBody);

        return true;
    }
    public async Task<bool> SendBanEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/BanUser.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber);

        await emailSender.SendEmailAsync(userDto.Email, Wc.BanAccount, htmlBody);

        return true;
    }
    public async Task<bool> SendTeacherConfirmEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/ConfirmTeacher.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber);

        await emailSender.SendEmailAsync(userDto.Email, Wc.TeacherConfirmEmail, htmlBody);

        return true;
    }
    public async Task<bool> SendHomeworkEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/UploadHomework.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber);

        await emailSender.SendEmailAsync(userDto.Email, Wc.HomeworkSendEmail, htmlBody);

        return true;
    }
    public async Task<bool> SendHomeworkReviewEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/HomeworkReview.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber);

        await emailSender.SendEmailAsync(userDto.Email, Wc.HomeworkReviewEmail, htmlBody);

        return true;
    }
    public async Task<bool> SendCreateCourseEmailAsync(UserDto userDto, string courseName, string type)
    {
        var templatePath = "Template/CourseCED.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{1}", userDto.FullName)
            .Replace("{2}", userDto.Email)
            .Replace("{3}", userDto.PhoneNumber)
            .Replace("{4}", courseName);

        switch (type)
        {
            case "edit":
                htmlBody.Replace("{0}", "Edit info");
                await emailSender.SendEmailAsync(userDto.Email, Wc.EditCourseEmail, htmlBody);
                break;
            case "delete":
                htmlBody.Replace("{0}", "was deleted");
                await emailSender.SendEmailAsync(userDto.Email, Wc.DeleteCourseEmail, htmlBody);
                break;
            case "create":
                htmlBody.Replace("{0}", "was created");
                await emailSender.SendEmailAsync(userDto.Email, Wc.CreateCourseEmail, htmlBody);
                break;
        }
        
        return true;
    }
    public async Task<bool> SendCreateAdminEmailAsync(UserDto userDto)
    {
        var templatePath = "Template/CreateAdmin.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber);

        await emailSender.SendEmailAsync(userDto.Email, Wc.CreateAdminEmail, htmlBody);

        return true;
    }
    public async Task<bool> SendMeetingEmailAsync(UserDto userDto, string teacherName, string courseName)
    {
        var templatePath = "Template/MeetingNotification.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", teacherName)
            .Replace("{1}", userDto.FullName)
            .Replace("{2}", courseName);

        await emailSender.SendEmailAsync(userDto.Email, Wc.MeetingNotificationEmail, htmlBody);

        return true;
    }
    
    public async Task<bool> SendScheduleEmailAsync(UserDto userDto, string teacherName, string courseName, DateTime dataCourse)
    {
        var templatePath = "Template/ScheduleNotification.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }
        
        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber)
            .Replace("{3}", courseName)
            .Replace("{4}", dataCourse.ToString())
            .Replace("{5}", teacherName);

        await emailSender.SendEmailAsync(userDto.Email, Wc.ScheduleNotificationEmail, htmlBody);

        return true;
    }
    
    public async Task<bool> SendPaymentConfirmationEmailAsync([FromQuery]UserDto userDto, [FromQuery]PaymentDto paymentDto)
    {
        var templatePath = "Template/Payment.html";
        var htmlBody = await templateReader.ReadTemplateAsync(templatePath);
        
        if (htmlBody == null)
        {
            return false;
        }

        htmlBody = htmlBody.Replace("{0}", userDto.FullName)
            .Replace("{1}", userDto.Email)
            .Replace("{2}", userDto.PhoneNumber)
            .Replace("{3}", paymentDto.TransactionId.ToString())
            .Replace("{4}", paymentDto.AmountPaid)
            .Replace("{5}", paymentDto.PaymentDate.ToString());

        await emailSender.SendEmailAsync(userDto.Email, Wc.PaymentNotificationEmail, htmlBody);

        return true;
    }
}