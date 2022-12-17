namespace Domain
{
    public class DtoRate : IDtoRate
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}