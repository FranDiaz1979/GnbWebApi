namespace Domain.Interfaces
{
    public interface ITransactionTotalDto
    {
        string Sku { get; set; }
        decimal Amount { get; set; }
    }
}