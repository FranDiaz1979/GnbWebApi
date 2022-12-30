namespace Entities
{
    public interface IRateEntity
    {
        string From { get; set; }
        string To { get; set; }
        decimal Rate { get; set; }
    }
}