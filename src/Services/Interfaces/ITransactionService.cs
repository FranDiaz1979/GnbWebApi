using Domain;

namespace Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetListAsysnc();
        Task<TransactionTotalDto> GetBySkuAsync(string sku);
    }
}
