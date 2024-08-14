using Proposal.API.Entities;
using Proposal.API.Common.Entities;
using Proposal.API.Interfaces;
using Newtonsoft.Json;

namespace Proposal.API.Services
{
    public class ProposalService : IProposalService
    {
        private readonly RabbitService rabbitService;

        public ProposalService(IConfiguration _configuration)
        {
            rabbitService = new RabbitService(_configuration);
        }

        public async Task<bool> ProcessMessage(string message)
        {
            try
            {
                var customer = JsonConvert.DeserializeObject<Customer>(message);

                if (customer == null)
                {
                    Console.WriteLine("Customer deserialized is null.");
                    throw new ArgumentNullException(nameof(customer), "Customer deserialized is null.");
                }

                if (!IsCustomerEligible(customer))
                {
                    var proposal = new CreditProposal
                    {
                        CustomerId = customer.CustomerId.ToString(),
                        CreditLimit = 0,
                        ProposalDate = DateTime.Now,
                        Name = customer.Name,
                        Status = Enum.StatusProposal.Reject,
                        Notes = "Customer is not eligible for a credit proposal."
                    };

                    //Publica a mensagem na fila para reply do cliente
                    await Task.Run(() => rabbitService.Publish("reply_customer_queue", JsonConvert.SerializeObject(proposal)));
                    return true;
                }

                var creditProposal = GenerateCreditProposal(customer);

                var proposalJson = JsonConvert.SerializeObject(creditProposal);

                //Publica a mensagem na fila para emissão do cartão
                await Task.Run(() => rabbitService.Publish("credit_queue", proposalJson));
                return true;
            }
            catch (Exception ex)
            {
            Console.WriteLine($"Error processing message: {ex.Message}");
            return true;
            }
        }


        private bool IsCustomerEligible(Customer customer)
        {
            if (!IsCustomerOfLegalAge(customer.DateBirth))
            {
                return false;
            }

            return true;
        }

        private bool IsCustomerOfLegalAge(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }

        private CreditProposal GenerateCreditProposal(Customer customer)
        {
            var proposal = new CreditProposal
            {
                CustomerId = customer.CustomerId.ToString(),
                CreditLimit = CalculateCreditLimit(customer),
                Name = customer.Name,
                ProposalDate = DateTime.Now,
                Status = Enum.StatusProposal.Approved
            };

            return proposal;
        }

        private decimal CalculateCreditLimit(Customer customer)
        {
            // Credit limit calculation based on 30% of the customer's income as credit limit
            decimal creditLimit = customer.Salary * 0.3m;

            return Math.Round(creditLimit, 2);

        }
    }
}