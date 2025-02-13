using FluentResults;
using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;
using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;
using GetUrCourse.Services.UserAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GetUrCourse.User.UnitTests;

public class UserControllerTests
{
	private readonly UserController _controller;
	private readonly Mock<ISender> _mockSender;

	public UserControllerTests()
	{
		_mockSender = new Mock<ISender>();
		_controller = new UserController(_mockSender.Object);
	}
	
}