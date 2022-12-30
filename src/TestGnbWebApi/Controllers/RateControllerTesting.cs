using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using WebApi.Controllers;
using WebApi.Interfaces;

namespace TestGnbWebApi.Controllers
{
    public class RateControllerTesting
    {
        private Microsoft.Extensions.Logging.ILogger<RateController>? logger;
        private IRateService? rateService;
        private IRateController? rateController;

        [SetUp]
        public void SetUp()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            this.logger = mockRepository.Create<Microsoft.Extensions.Logging.ILogger<RateController>>().Object;
            IEnumerable<RateEntity> rate = new List<RateEntity>() {
                new RateEntity() { From = "EUR", To = "USD", Rate = (decimal)1.525 },
            };
            var mock = mockRepository.Create<IRateService>();
            mock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(rate));
            rateService = mock.Object;
            rateController = new RateController(logger, rateService);
        }

        [Test]
        public async Task Get_Ok()
        {
            Assert.That(rateController, Is.Not.Null);

            var response = await rateController!.Get();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            var value = ((OkObjectResult)response).Value;
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IEnumerable<RateEntity>>());

            var rateList = (IEnumerable<RateEntity>)value!;
            Assert.That(rateList, Is.Not.Null);

            var rate = rateList!.FirstOrDefault();
            Assert.That(rate, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rate?.From, Is.EqualTo("EUR"));
                Assert.That(rate?.To, Is.EqualTo("USD"));
                Assert.That(rate?.Rate, Is.EqualTo(1.525));
            });
        }
    }
}