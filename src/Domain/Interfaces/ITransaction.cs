namespace Entities
{
    public interface ITransaction
    {
        string Sku { get; set; }
        decimal Amount { get; set; }
        string Currency { get; set; }
    }
}