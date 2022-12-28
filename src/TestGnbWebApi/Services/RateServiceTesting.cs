namespace TestGnbWebApi.Services
{
    using Domain;
    using global::Domain;
    using global::Services;
    using Infraestructure;
    using Moq;
    using WebApi.Controllers;

    public class RateServiceTesting
    {
        private IApiClient _apiClient;
        private IRateService rateService;
        
        [SetUp]
        public void Setup()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            this._apiClient = mockRepository.Create<IApiClient>().Object;

            this.rateService = new RateService(_apiClient);
        }

        [Test]
        public void TestingWorking()
        {
            Assert.Pass();
        }

        [Test]
        public void GetListAsync_Ok()
        {
            var result = rateService.GetListAsync();
            result.Wait();
            Assert.That(result, Is.Not.Null, "Task de GetListAsync ha sido nula");

            var rateList = result.Result;
            Assert.That(rateList, Is.Not.Null, "El resultado de GetListAsync ha sido inesperadamente nulo");

            var firstRate =  rateList.First();
            Assert.Multiple(() =>
            {
                Assert.That(firstRate.From, Is.EqualTo("EUR"));
                Assert.That(firstRate.To, Is.EqualTo("USD"));
                Assert.That(firstRate.Rate, Is.EqualTo(1.359));
            });
        }

        [Test]
        public void AmountToEur_Ok() 
        {
            var result = rateService.AmountToEur((decimal)10.10,"USD").Result;
            Assert.That(result, Is.EqualTo(7.4336));

            result = rateService.AmountToEur((decimal)10.10, "CAD").Result;
            Assert.That(result, Is.EqualTo(7.3932));
        }

        [Test]
        public void AmountToEur_Eur()
        {
            var result = rateService.AmountToEur((decimal)10.10, "EUR").Result;
            Assert.That(result, Is.EqualTo(10.10));
        }

        [Test]
        public void AmountToEur_Nok() 
        {
            var task = rateService.AmountToEur((decimal)10.10,"ZZZ");
            task.Wait();
            Assert.That(task,Is.Not.Null);

            var result = task.Result;
            Assert.That(result, Is.Zero);
        }
    }
}