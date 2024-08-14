
namespace Customer.API.Common.Entities
{
    public class CreditProposal
    {
        public string ProposalId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
