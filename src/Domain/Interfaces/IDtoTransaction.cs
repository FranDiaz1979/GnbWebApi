namespace Domain
{
    public interface IDtoTransaction
    {
        decimal Amount { get; set; }
        string Currency { get; set; }
        string Sku { get; set; }
    }
}