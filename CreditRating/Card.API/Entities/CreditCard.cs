
namespace Card.API.Entities
{

    public class CreditCard
    {
        public Guid CardId { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CustomerId { get; set; }
        public string ProposalId { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime IssueDate { get; set; }

        public CreditCard()
        {
            CardId = Guid.NewGuid();
        }
    }
}


