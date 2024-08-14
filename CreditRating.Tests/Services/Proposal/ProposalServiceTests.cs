using Xunit;
using Moq;
using System.Threading.Tasks;
using Proposal.API.Services;
using Proposal.API.Entities;
using Proposal.API.Common.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace Proposal.API.Tests.Services
{
    public class ProposalServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ProposalService _proposalService;

        public ProposalServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _proposalService = new ProposalService(_mockConfiguration.Object);
        }

        [Fact]
        public async Task ProcessMessage_ValidCustomer_Eligible_ReturnsTrue()
        {
            var customer = new Proposal.API.Common.Entities.Customer
            {
                CustomerId = "12345",
                Name = "Renata Felix",
                DateBirth = new DateTime(1990, 1, 1),
                Salary = 5000
            };

            string message = JsonConvert.SerializeObject(customer);

            var result = await _proposalService.ProcessMessage(message);

            Assert.True(result);
        }

    }
}
