namespace TestGnbWebApi.Domain
{
    using Domain;
    using global::Domain;

    public class DomainTesting
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void TestingWorking()
        {
            Assert.Pass();
        }   

        [Test]
        public void TestRateDto()
        {
            RateDto rate = new RateDto()
            {
                From = "USD",
                To = "EUR",
                Rate = (decimal)1.565,
            };

            Assert.That(rate.From, Is.EqualTo("USD"));
            Assert.That(rate.To, Is.EqualTo("EUR"));
            Assert.That(rate.Rate, Is.EqualTo(1.565));
        }
    }
}