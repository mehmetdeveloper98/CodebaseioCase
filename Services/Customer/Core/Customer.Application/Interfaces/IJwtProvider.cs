namespace Customer.Application.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> CreateTokenAsync(Domain.Entities.Customer customer);
    }
}
