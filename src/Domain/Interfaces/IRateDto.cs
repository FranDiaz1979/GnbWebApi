namespace Domain
{
    public interface IRateDto
    {
        string From { get; set; }
        string To { get; set; }
        decimal Rate { get; set; }
    }
}