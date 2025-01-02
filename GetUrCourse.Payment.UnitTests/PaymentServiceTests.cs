using GetUrCourse.Services.PaymentAPI.Constants;
using GetUrCourse.Services.PaymentAPI.Infrastructure.Repositories;
using GetUrCourse.Services.PaymentAPI.Models;
using Moq;
using Newtonsoft.Json;
using PaymentAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class PaymentServiceTests
{
	private readonly Mock<IPaymentRepository> _mockRepository;
	private readonly PaymentService _paymentService;

	public PaymentServiceTests()
	{
		_mockRepository = new Mock<IPaymentRepository>();
		_paymentService = new PaymentService("testPublicKey", "testPrivateKey", _mockRepository.Object);
	}

	[Fact]
	public async Task CreatePaymentAsync_ShouldReturnPaymentUrlAndSavePayment()
	{
		// Arrange
		string orderId = "12345";
		string action = "pay";
		decimal amount = 100.50m;
		string description = "Test Payment";

		_mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);

		// Act
		var result = await _paymentService.CreatePaymentAsync(orderId, action, amount, description);

		// Assert
		Assert.Contains("https://www.liqpay.ua/api/3/checkout", result);
		_mockRepository.Verify(repo => repo.AddAsync(It.Is<Payment>(p =>
			p.OrderId == orderId &&
			p.Action == action &&
			p.Amount == amount &&
			p.Description == description &&
			p.Status == PaymentSettings.IsCreated
		)), Times.Once);
	}

	[Fact]
	public async Task HandlePaymentResultAsync_ShouldReturnFalse_WhenPaymentNotFound()
	{
		// Arrange
		string orderId = "12345";
		var requestDictionary = new Dictionary<string, string>
		{
			{"data", Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
			{
				version = "3",
				order_id = orderId,
				status = "success"
			})))},
			{"signature", "testSignature"}
		};

		_mockRepository.Setup(repo => repo.GetByOrderIdAsync(orderId)).ReturnsAsync((Payment)null);

		// Act
		var result = await _paymentService.HandlePaymentResultAsync(requestDictionary);

		// Assert
		Assert.False(result);
	}


	[Fact]
	public async Task UnsubscribeAsync_ShouldReturnFalse_WhenPaymentNotFound()
	{
		// Arrange
		string orderId = "12345";

		_mockRepository.Setup(repo => repo.GetByOrderIdAsync(orderId)).ReturnsAsync((Payment)null);

		// Act
		var result = await _paymentService.UnsubscribeAsync(orderId);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public async Task CreatePaymentAsync_ShouldThrowException_WhenAmountIsNegative()
	{
		// Arrange
		string orderId = "12345";
		string action = "pay";
		decimal amount = -50;
		string description = "Test Payment";

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() =>
			_paymentService.CreatePaymentAsync(orderId, action, amount, description));
	}

	[Fact]
	public async Task HandlePaymentResultAsync_ShouldThrowException_WhenDataIsInvalid()
	{
		// Arrange
		var requestDictionary = new Dictionary<string, string>
		{
			{"data", "InvalidBase64Data"},
			{"signature", "testSignature"}
		};

		// Act & Assert
		await Assert.ThrowsAsync<FormatException>(() =>
			_paymentService.HandlePaymentResultAsync(requestDictionary));
	}
}
