namespace TestGnbWebApi.Services
{
    using global::Services;
    using Infraestructure;
    using Moq;

    public class RateServiceTesting
    {
        private IApiClient? _apiClient;
        private IRateService? _rateService;

        [SetUp]
        public void Setup()
        {
            //var mockRepository = new MockRepository(MockBehavior.Default);
            //this._apiClient = mockRepository.Create<IApiClient>().Object;

            this._apiClient = new ApiClient();
            this._rateService = new RateService(_apiClient);
        }

        [Test]
        public void TestingWorking()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAllAsync_Ok()
        {
            Assert.That(_rateService, Is.Not.Null);

            var result = _rateService!.GetAllAsync();
            result.Wait();
            Assert.That(result, Is.Not.Null, "Task de GetAllAsync ha sido nula");

            var rateList = result.Result;
            Assert.That(rateList, Is.Not.Null, "El resultado de GetAllAsync ha sido inesperadamente nulo");

            var firstRate = rateList.First(x=>x.From=="EUR" && x.To=="USD");
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
            Assert.That(_rateService, Is.Not.Null);
            
            var result = _rateService!.AmountToEur((decimal)10.10, "USD").Result;
            Assert.That(result, Is.EqualTo(7.4336));

            result = _rateService!.AmountToEur((decimal)10.10, "CAD").Result;
            Assert.That(result, Is.EqualTo(7.3932));
        }

        [Test]
        public void AmountToEur_Eur()
        {
            Assert.That(_rateService, Is.Not.Null);
            
            var result = _rateService!.AmountToEur((decimal)10.10, "EUR").Result;
            Assert.That(result, Is.EqualTo(10.10));
        }

        [Test]
        public void AmountToEur_Nok()
        {
            Assert.That(_rateService, Is.Not.Null);

            var task = _rateService!.AmountToEur((decimal)10.10, "ZZZ");
            task.Wait();
            Assert.That(task, Is.Not.Null);

            var result = task.Result;
            Assert.That(result, Is.Zero);
        }
    }
}