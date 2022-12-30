namespace Entities
{
    public class RateEntity : IRateEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }

        public RateEntity()
        {
            From = string.Empty;
            To = string.Empty;
        }
    }
}