
namespace Customer.API.Common.Entities
{
    public class CreditCard
    {
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProposalId { get; set; }
        public string CardId { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime IssueDate { get; set; }
    }
}


