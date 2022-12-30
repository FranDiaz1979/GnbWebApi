namespace TestGnbWebApi.Domain
{
    using Entities;

    public class DomainTesting
    {
        [Test]
        public void TestingWorking()
        {
            Assert.Pass();
        }

        [Test]
        public void TestRate()
        {
            RateEntity rate = new()
            {
                From = "USD",
                To = "EUR",
                Rate = (decimal)1.565,
            };
            Assert.Multiple(() =>
            {
                Assert.That(rate.From, Is.EqualTo("USD"));
                Assert.That(rate.To, Is.EqualTo("EUR"));
                Assert.That(rate.Rate, Is.EqualTo(1.565));
            });
        }
    }
}