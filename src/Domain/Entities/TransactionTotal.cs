namespace Entities
{
    public class TransactionTotal : ITransactionTotal
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }

        public TransactionTotal()
        {
            Sku = string.Empty;
        }
    }
}