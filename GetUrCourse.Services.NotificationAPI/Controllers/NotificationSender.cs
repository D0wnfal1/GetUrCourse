using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.NotificationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationSenderController(INotificationService notificationService) : ControllerBase
    {
        [HttpPost("send_confirm_email")]
        public async Task<IActionResult> SendConfirmEmail(UserDto userDto)
        {
            var result = await notificationService.SendConfirmEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }

        [HttpPost("send_register_course_email")]
        public async Task<IActionResult> SendRegisterCourseEmail(UserDto userDto, string courseName)
        {
            var result = await notificationService.SendRegisterCourseEmailAsync(userDto, courseName);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_ban_email")]
        public async Task<IActionResult> SendBanEmail(UserDto userDto)
        {
            var result = await notificationService.SendBanEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_teacher_confirm_email")]
        public async Task<IActionResult> SendTeacherConfirmEmail(UserDto userDto)
        {
            var result = await notificationService.SendTeacherConfirmEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_homework_email")]
        public async Task<IActionResult> SendHomeworkEmail(UserDto userDto)
        {
            var result = await notificationService.SendHomeworkEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_homework_review_email")]
        public async Task<IActionResult> SendHomeworkReviewEmail(UserDto userDto)
        {
            var result = await notificationService.SendHomeworkReviewEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_course_crud_email")]
        public async Task<IActionResult> SendHomeworkReviewEmail(UserDto userDto, string courseName, string type)
        {
            var result = await notificationService.SendCreateCourseEmailAsync(userDto, courseName, type);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_create_new_admin_email")]
        public async Task<IActionResult> SendCreateAdminEmail(UserDto userDto)
        {
            var result = await notificationService.SendCreateAdminEmailAsync(userDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_meeting_notification_email")]
        public async Task<IActionResult> SendMeetingNotificationEmail(UserDto userDto, string teacherName, string courseName)
        {
            var result = await notificationService.SendMeetingEmailAsync(userDto, teacherName, courseName);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_schedule_notification_email")]
        public async Task<IActionResult> SendScheduleNotificationEmail(UserDto userDto, string teacherName, string courseName, DateTime dataCourse)
        {
            var result = await notificationService.SendScheduleEmailAsync(userDto, teacherName, courseName, dataCourse);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        [HttpPost("send_payment_notification_email")]
        public async Task<IActionResult> SendPaymentNotificationEmail(UserDto userDto, PaymentDto paymentDto)
        {
            var result = await notificationService.SendPaymentConfirmationEmailAsync(userDto, paymentDto);

            if (result)
            {
                return Ok();
            }

            return NotFound("Template file not found.");
        }
        
        
    }
}