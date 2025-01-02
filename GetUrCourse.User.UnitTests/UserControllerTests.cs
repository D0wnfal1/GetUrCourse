using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;
using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;
using GetUrCourse.Services.UserAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using FluentResults;
using Xunit;

namespace GetUrCourse.Services.UserAPI.Tests.Controllers
{
	public class UserControllerTests
	{
		private readonly UserController _controller;
		private readonly Mock<ISender> _mockSender;

		public UserControllerTests()
		{
			_mockSender = new Mock<ISender>();
			_controller = new UserController(_mockSender.Object);
		}

		[Fact]
		public async Task CreateUser_ShouldReturnOk_WhenCommandIsSuccessful()
		{
			// Arrange
			var createUserCommand = new CreateUserCommand
			{
				UserName = "newuser",
				Email = "newuser@example.com",
				Password = "Password123"
			};
			var result = Result.Ok(new UserCreateResponse { Id = 1, UserName = "newuser" });

			_mockSender.Setup(sender => sender.Send(It.IsAny<CreateUserCommand>(), default))
				.ReturnsAsync(result);

			// Act
			var response = await _controller.CreateUser(createUserCommand);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(response);
			var returnValue = Assert.IsType<UserCreateResponse>(okResult.Value);
			Assert.Equal(1, returnValue.Id);
			Assert.Equal("newuser", returnValue.UserName);
		}

		[Fact]
		public async Task UpdateUser_ShouldReturnOk_WhenCommandIsSuccessful()
		{
			// Arrange
			var updateUserCommand = new UpdateUserCommand
			{
				UserId = 1,
				UserName = "updateduser",
				Email = "updateduser@example.com"
			};
			var result = Result.Ok();

			_mockSender.Setup(sender => sender.Send(It.IsAny<UpdateUserCommand>(), default))
				.ReturnsAsync(result);

			// Act
			var response = await _controller.UpdateUser(updateUserCommand);

			// Assert
			Assert.IsType<OkResult>(response);
		}


		[Fact]
		public async Task UpdateUser_ShouldReturnOk_WhenCommandIsSuccessful()
		{
			// Arrange
			var updateUserCommand = new UpdateUserCommand
			{
				UserId = 1,
				UserName = "updateduser",
				Email = "updateduser@example.com"
			};
			var result = Result.Ok();

			_mockSender.Setup(sender => sender.Send(It.IsAny<UpdateUserCommand>(), default))
				.ReturnsAsync(result);

			// Act
			var response = await _controller.UpdateUser(updateUserCommand);

			// Assert
			Assert.IsType<OkResult>(response);
		}

		[Fact]
		public async Task UpdateUser_ShouldReturnBadRequest_WhenCommandFails()
		{
			// Arrange
			var updateUserCommand = new UpdateUserCommand
			{
				UserId = 1,
				UserName = "updateduser",
				Email = "updateduser@example.com"
			};
			var result = Result.Fail("Error updating user");

			_mockSender.Setup(sender => sender.Send(It.IsAny<UpdateUserCommand>(), default))
				.ReturnsAsync(result);

			// Act
			var response = await _controller.UpdateUser(updateUserCommand);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
			Assert.Equal("Error updating user", badRequestResult.Value);
		}
	}
}
