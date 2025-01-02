using GetUrCourse.Services.TaskAPI.Controllers;
using GetUrCourse.Services.TaskAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class FilesControllerTests
{
	private readonly FilesController _controller;
	private readonly Mock<IFileService> _mockFileService;

	public FilesControllerTests()
	{
		_mockFileService = new Mock<IFileService>();
		_controller = new FilesController(_mockFileService.Object);
	}

	[Fact]
	public async Task UploadFile_ShouldReturnBadRequest_WhenFileIsInvalid()
	{
		// Arrange
		IFormFile mockFile = null;

		_mockFileService.Setup(s => s.UploadFileAsync(mockFile)).ThrowsAsync(new ArgumentException("File is invalid"));

		// Act
		var result = await _controller.UploadFile(mockFile);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal("File is invalid", badRequestResult.Value);
	}
}
