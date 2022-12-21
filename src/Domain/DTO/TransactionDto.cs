namespace Domain
{
    public class TransactionDto : ITransactionDto
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
