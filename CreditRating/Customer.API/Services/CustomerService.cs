using Customer.API.Common.Entities;
using Customer.API.Entities;
using Customer.API.Interfaces;
using Newtonsoft.Json;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private RabbitService rabbitService;

        public CustomerService(IConfiguration _configuration)
        {
            rabbitService = new RabbitService(_configuration);
        }

        public async Task<bool> CreateCustomer(CustomerData customer)
        {
            await Task.Run(() => rabbitService.Publish(JsonConvert.SerializeObject(customer)));

            return true;
        }

        // Method executed from the Customer Worker execution.
        // Return of proposal or card
        public async Task<bool> ProcessMessage(string origin, string message)
        {
            try
            {
                if(origin == "Proposal.API")
                {
                    var proposal = JsonConvert.DeserializeObject<CreditProposal>(message);
                    Console.WriteLine("-------x Proposal Status x-------");
                    Console.WriteLine(JsonConvert.SerializeObject(proposal, Formatting.Indented));
                }

                else if(origin == "Card.API")
                {
                    var card = JsonConvert.DeserializeObject<CreditCard>(message);
                    Console.WriteLine("-------x Credit Card Status x-------");
                    Console.WriteLine(JsonConvert.SerializeObject(card, Formatting.Indented));
                }

                else
                {
                    Console.WriteLine("Unknown origin");
                }
                
                return true;

            }
            catch (Exception ex)
            {
            Console.WriteLine($"Error processing message: {ex.Message}");
            return false;
            }
        }

    }
}