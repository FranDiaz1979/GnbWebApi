using Domain.Interfaces;

namespace Domain
{
    public class TransactionTotalDto : ITransactionTotalDto
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
    }
}
