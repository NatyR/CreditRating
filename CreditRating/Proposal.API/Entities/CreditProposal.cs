using Proposal.API.Enum;

namespace Proposal.API.Entities
{
    public class CreditProposal
    {
        public Guid ProposalId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime ProposalDate { get; set; }
        public StatusProposal Status { get; set; }
        public string Notes { get; set; }


        public CreditProposal()
        {
            ProposalId = Guid.NewGuid();
        }

    }
}
