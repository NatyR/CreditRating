using Card.API.Common.Entities;
using Card.API.Entities;
using Card.API.Interfaces;
using Newtonsoft.Json;


namespace Card.API.Services
{
    public class CardService : ICardService
    {
        private readonly RabbitService rabbitService;

        public CardService(IConfiguration _configuration)
        {
            rabbitService = new RabbitService(_configuration);
        }

        // Method executed from the Card Worker execution.
        // Card Number Generation
        // CVV generation
        public async Task<bool> ProcessMessage(string message)
        {
            try
            {
                var proposal = JsonConvert.DeserializeObject<CreditProposal>(message);

                if (proposal!= null  && proposal.Status == Common.Enum.StatusProposal.Approved)
                {
                    var card = IssueCard(proposal);

                    await Task.Run(() => rabbitService.Publish(JsonConvert.SerializeObject(card)));
                }
                
                return true;

            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                            return false;

            }
            catch (Exception ex)
            {
            Console.WriteLine($"Error processing message: {ex.Message}");
            return false;
            }
        }

        private CreditCard IssueCard(CreditProposal proposal)
        {
            var card = new CreditCard
            {
                Name = proposal.Name,
                CustomerId = proposal.CustomerId,
                ProposalId = proposal.ProposalId,
                CardNumber = GenerateCardNumber(),
                ExpirationDate = DateTime.Now.AddYears(3),
                CVV = GenerateCVV(),
                CreditLimit = proposal.CreditLimit,
                IssueDate = DateTime.Now
            };

            return card;
        }

        private string GenerateCardNumber()
        {
            var random = new Random();
                
            string part1 = random.Next(1000, 9999).ToString("D4");
            string part2 = random.Next(1000, 9999).ToString("D4");
            string part3 = random.Next(1000, 9999).ToString("D4");
            string part4 = random.Next(1000, 9999).ToString("D4");

            string cardNumber = $"{part1}-{part2}-{part3}-{part4}";

            return cardNumber;        
        }

        private string GenerateCVV()
        {
            var random = new Random();
            string cvv = random.Next(100, 999).ToString("D3");

            return cvv;
            
        }
    }
}