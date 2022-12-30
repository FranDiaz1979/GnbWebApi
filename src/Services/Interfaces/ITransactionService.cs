using Entities;

namespace Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsysnc();

        Task<TransactionTotal> GetBySkuAsync(string sku);
    }
}