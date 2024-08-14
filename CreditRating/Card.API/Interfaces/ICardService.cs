namespace Card.API.Interfaces
{
    public interface ICardService
    {
        Task<bool> ProcessMessage(string message);
    }
}
