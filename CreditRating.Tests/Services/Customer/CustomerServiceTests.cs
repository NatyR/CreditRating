using Xunit;
using Moq;
using System.Threading.Tasks;
using Customer.API.Services;
using Customer.API.Entities;
using Customer.API.Common.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace Customer.API.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _customerService = new CustomerService(_mockConfiguration.Object);
        }


        [Fact]
        public async Task ProcessMessage_ValidProposalOrigin_ReturnsTrue()
        {
            var proposal = new CreditProposal
            {
                Status = "Approved",
                CustomerId = "12345",
                ProposalId = "12345",
                CreditLimit = 10000
            };
            string message = JsonConvert.SerializeObject(proposal);

            var result = await _customerService.ProcessMessage("Proposal.API", message);

            Assert.True(result);
        }

        [Fact]
        public async Task ProcessMessage_ValidCardOrigin_ReturnsTrue()
        {
            var card = new CreditCard
            {
                Name = "Renata Felix",
                CustomerId = Guid.NewGuid(),
                ProposalId = Guid.NewGuid(),
                CardNumber = "1234-5678-9012-3456",
                CreditLimit = 10000
            };
            string message = JsonConvert.SerializeObject(card);

            var result = await _customerService.ProcessMessage("Card.API", message);

            Assert.True(result);
        }

        [Fact]
        public async Task ProcessMessage_UnknownOrigin_ReturnsTrue()
        {
            string message = "{}";

            var result = await _customerService.ProcessMessage("Unknown.API", message);

            Assert.True(result);
        }

        [Fact]
        public async Task ProcessMessage_InvalidJson_ReturnsFalse()
        {
            string invalidMessage = "{ invalid json";

            var result = await _customerService.ProcessMessage("Proposal.API", invalidMessage);

            Assert.False(result);
        }
    }
}
