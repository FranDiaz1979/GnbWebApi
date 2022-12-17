namespace Domain
{
    public interface IDtoRate
    {
        string From { get; set; }
        decimal Rate { get; set; }
        string To { get; set; }
    }
}