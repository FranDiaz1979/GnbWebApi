using Domain;

namespace Services
{
    public interface IRateService
    {
        Task<IEnumerable<RateDto>> GetListAsync();
        Task<decimal> AmountToEur(decimal amount, string currency);
    }
}