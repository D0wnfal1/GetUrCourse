using Xunit;
using Moq;
using GetUrCourse.Services.NotificationAPI.Consumers;
using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using GetUrCourse.Contracts.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class NotifyConsumerTests
{
	private readonly Mock<INotificationService> _notificationServiceMock;
	private readonly Mock<ILogger<NotifyConsumer>> _loggerMock;
	private readonly Mock<ConsumeContext<NotifyUser>> _consumeContextMock;
	private readonly NotifyConsumer _consumer;

	public NotifyConsumerTests()
	{
		_notificationServiceMock = new Mock<INotificationService>();
		_loggerMock = new Mock<ILogger<NotifyConsumer>>();
		_consumeContextMock = new Mock<ConsumeContext<NotifyUser>>();
		_consumer = new NotifyConsumer(_notificationServiceMock.Object, _loggerMock.Object);
	}

	[Fact]
	public async Task Consume_ShouldCallServiceAndPublishEvent()
	{
		// Arrange
		var notifyUser = new NotifyUser(Guid.NewGuid(), "test@example.com", "Test User");
		_consumeContextMock.Setup(c => c.Message).Returns(notifyUser);
		_notificationServiceMock.Setup(s => s.SendConfirmEmailAsync(It.IsAny<UserDto>()))
			.ReturnsAsync(true);

		// Act
		await _consumer.Consume(_consumeContextMock.Object);

		// Assert
		_notificationServiceMock.Verify(s => s.SendConfirmEmailAsync(It.Is<UserDto>(
			u => u.Email == notifyUser.Email && u.FullName == notifyUser.FullName
		)), Times.Once);

		_consumeContextMock.Verify(c => c.Publish(It.Is<UserNotified>(
			e => e.UserId == notifyUser.UserId && e.Email == notifyUser.Email
		), default), Times.Once);

		_loggerMock.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("User notifying started")),
				null,
				It.IsAny<Func<It.IsAnyType, Exception, string>>()
			),
			Times.Once);

		_loggerMock.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("User notifying finished")),
				null,
				It.IsAny<Func<It.IsAnyType, Exception, string>>()
			),
			Times.Once);
	}

}