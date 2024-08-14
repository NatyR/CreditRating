using Xunit;
using System.Threading.Tasks;
using Card.API.Services;
using Card.API.Common.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using Moq;


namespace Card.API.Tests.Services
{
    public class CardServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly CardService _cardService;

        public CardServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _cardService = new CardService(_mockConfiguration.Object);
        }

        [Fact]
        public async Task ProcessMessage_InvalidJson_ReturnsFalse()
        {
            string invalidMessage = "{ invalid json";

            var result = await _cardService.ProcessMessage(invalidMessage);

            Assert.False(result);
        }

        [Fact]
        public async Task ProcessMessage_NonApprovedProposal_ReturnsTrue()
        {
            var proposal = new CreditProposal
            {
                Status = Common.Enum.StatusProposal.Pending,
                Name = "Renata Felix",
                CustomerId = "12345",
                ProposalId = "12345",
                CreditLimit = 5000
            };

            string message = JsonConvert.SerializeObject(proposal);

            var result = await _cardService.ProcessMessage(message);

            Assert.True(result);
        }
    }
}
