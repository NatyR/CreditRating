using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Customer.API.Controllers;
using Customer.API.Interfaces;
using Customer.API.Entities;
using System.Threading.Tasks;
using System;


namespace Customer.API.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task CreateCustomer_ValidCustomer_ReturnsOk()
        {
            var customer = new CustomerData
            {
                Name = "Renata Felix",
                Email = "renata@example.com",
                Address = "Rua Teste, 123, Sao Paulo - SP",
                Telephone = "123-456-7890"
            };

            _mockCustomerService.Setup(service => service.CreateCustomer(customer))
                                .ReturnsAsync(true); // Simulando o comportamento do serviço

            // Act
            var result = await _controller.CreateCustomer(customer);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Request successfully. Wait for proposal analysis.", actionResult.Value);
        }

        [Fact]
        public async Task CreateCustomer_ServiceThrowsException_ReturnsBadRequest()
        {
            var customer = new CustomerData
            {
                Name = "Renata Felix",
                Email = "renata@example.com",
                Address = "Rua Teste, 123, Sao Paulo - SP",
                Telephone = "123-456-7890"
            };

            _mockCustomerService.Setup(service => service.CreateCustomer(customer))
                                .ThrowsAsync(new Exception("Customer creation failed"));

            var result = await _controller.CreateCustomer(customer);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Customer creation failed", actionResult.Value);
        }
    }
}
