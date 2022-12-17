namespace Domain
{
    public class RateDto : IRateDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}