using Entities;

namespace Services
{
    public interface IRateService
    {
        Task<IEnumerable<RateEntity>> GetAllAsync();

        Task<decimal> AmountToEur(decimal amount, string currency);
    }
}