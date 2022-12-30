namespace Entities
{
    public interface ITransactionTotal
    {
        string Sku { get; set; }
        decimal Amount { get; set; }
    }
}