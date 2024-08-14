namespace Proposal.API.Interfaces
{
    public interface IProposalService
    {
        Task<bool> ProcessMessage(string message);
    }
}
