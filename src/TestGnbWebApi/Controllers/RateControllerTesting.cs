using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using WebApi.Controllers;
using WebApi.Interfaces;

namespace TestGnbWebApi.Controllers
{
    public class RateControllerTesting
    {
        private  Microsoft.Extensions.Logging.ILogger<RatesController> logger;
        private  IRateService rateService;
        private  IRatesController ratesController;

 

        [SetUp]
        public void SetUp() 
        {
            var mockRepository= new MockRepository(MockBehavior.Default);
            this.logger = mockRepository.Create<Microsoft.Extensions.Logging.ILogger<RatesController>>().Object;
            IEnumerable<RateDto> rate = new List<RateDto>() {
                new RateDto() { From = "EUR", To = "USD", Rate = (decimal)1.525 },
            };
            var mock = mockRepository.Create<IRateService>();
            mock.Setup(x=>x.GetListAsync()).Returns(Task.FromResult(rate));
            rateService = mock.Object;
            ratesController = new RatesController(logger, rateService);
        }

        [Test]
        public async Task Get_Ok()
        {
            var response = await ratesController.Get();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            var value = ((OkObjectResult)response).Value;
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IEnumerable<RateDto>>());

            var rateDto = ((IEnumerable<RateDto>)value).FirstOrDefault();
            Assert.That(rateDto, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rateDto.From, Is.EqualTo("EUR"));
                Assert.That(rateDto.To, Is.EqualTo("USD"));
                Assert.That(rateDto.Rate, Is.EqualTo(1.525));
            });
        }

        //[Test]
        //public async Task Get_Nok204()
        //{
        //    var result = await ratesController.Get();

        //    Assert.Fail();
        //}

        //[Test]
        //public async Task Get_Nok404()
        //{
        //    var result = await ratesController.Get();

        //    Assert.Fail();
        //}

        [TearDown]
        public void TearDown() 
        {
            
        }
    }
}
