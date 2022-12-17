namespace Domain
{
    public interface ITransactionDto
    {
        string Sku { get; set; }
        decimal Amount { get; set; }
        string Currency { get; set; }
    }
}