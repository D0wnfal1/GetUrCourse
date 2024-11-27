using GetUrCourse.Services.NotificationAPI.Dto;

namespace GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;

public interface INotificationService
{
    Task<bool> SendConfirmEmailAsync(UserDto userDto);
    Task<bool> SendRegisterCourseEmailAsync(UserDto userDto, string courseName);
    Task<bool> SendBanEmailAsync(UserDto userDto);
    Task<bool> SendTeacherConfirmEmailAsync(UserDto userDto);
    Task<bool> SendHomeworkEmailAsync(UserDto userDto);
    Task<bool> SendHomeworkReviewEmailAsync(UserDto userDto);
    Task<bool> SendCreateCourseEmailAsync(UserDto userDto, string courseName, string type);
    Task<bool> SendCreateAdminEmailAsync(UserDto userDto);
    Task<bool> SendMeetingEmailAsync(UserDto userDto, string teacherName, string courseName);
    Task<bool> SendScheduleEmailAsync(UserDto userDto, string teacherName, string courseName, DateTime dataCourse);
    Task<bool> SendPaymentConfirmationEmailAsync(UserDto userDto, PaymentDto paymentDto);
}