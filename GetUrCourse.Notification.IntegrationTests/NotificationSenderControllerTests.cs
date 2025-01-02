using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using GetUrCourse.Services.NotificationAPI.Controllers;
using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using System.Threading.Tasks;

public class NotificationSenderControllerTests
{
	private readonly Mock<INotificationService> _notificationServiceMock;
	private readonly NotificationSenderController _controller;

	public NotificationSenderControllerTests()
	{
		_notificationServiceMock = new Mock<INotificationService>();
		_controller = new NotificationSenderController(_notificationServiceMock.Object);
	}

	[Fact]
	public async Task SendConfirmEmail_ShouldReturnOk_WhenServiceReturnsTrue()
	{
		// Arrange
		var userDto = new UserDto { Email = "test@example.com", FullName = "Test User" };
		_notificationServiceMock.Setup(s => s.SendConfirmEmailAsync(userDto)).ReturnsAsync(true);

		// Act
		var result = await _controller.SendConfirmEmail(userDto);

		// Assert
		Assert.IsType<OkResult>(result);
	}

	[Fact]
	public async Task SendConfirmEmail_ShouldReturnNotFound_WhenServiceReturnsFalse()
	{
		// Arrange
		var userDto = new UserDto { Email = "test@example.com", FullName = "Test User" };
		_notificationServiceMock.Setup(s => s.SendConfirmEmailAsync(userDto)).ReturnsAsync(false);

		// Act
		var result = await _controller.SendConfirmEmail(userDto);

		// Assert
		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal("Template file not found.", notFoundResult.Value);
	}

	[Fact]
	public async Task SendRegisterCourseEmail_ShouldCallServiceWithCorrectParameters()
	{
		// Arrange
		var userDto = new UserDto { Email = "test@example.com", FullName = "Test User" };
		var courseName = "C# Basics";
		_notificationServiceMock.Setup(s => s.SendRegisterCourseEmailAsync(userDto, courseName)).ReturnsAsync(true);

		// Act
		var result = await _controller.SendRegisterCourseEmail(userDto, courseName);

		// Assert
		_notificationServiceMock.Verify(s => s.SendRegisterCourseEmailAsync(userDto, courseName), Times.Once);
		Assert.IsType<OkResult>(result);
	}
}
