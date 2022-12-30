namespace Entities
{
    public class Transaction : ITransaction
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Transaction()
        {
            Sku = string.Empty;
            Currency = string.Empty;
        }
    }
}