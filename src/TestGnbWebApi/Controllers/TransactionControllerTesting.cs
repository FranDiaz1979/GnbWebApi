using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using WebApi.Controllers;

namespace TestGnbWebApi.Controllers
{
    public class TransactionControllerTesting
    {
        private Microsoft.Extensions.Logging.ILogger<TransactionController>? logger;
        private ITransactionService? transactionService;
        private TransactionController? transactionController;

        [SetUp]
        public void SetUp()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            this.logger = mockRepository.Create<Microsoft.Extensions.Logging.ILogger<TransactionController>>().Object;
            var mock = mockRepository.Create<ITransactionService>();
            IEnumerable<Transaction> transactionDtos = new List<Transaction>()
            {
                new Transaction(){
                    Sku="T2006",
                    Amount=(decimal)11.20,
                    Currency="EUR",
                },
            };
            mock.Setup(x => x.GetAllAsysnc()).Returns(Task.FromResult(transactionDtos));
            this.transactionService = mock.Object;
            transactionController = new TransactionController(logger, transactionService);
        }

        [Test]
        public async Task Get_Ok()
        {
            Assert.That(transactionController, Is.Not.Null);

            var response = await transactionController!.Get();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            var value = ((OkObjectResult)response).Value;
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IEnumerable<Transaction>>());

            var transactionList = (IEnumerable<Transaction>)value!;
            Assert.That(transactionList, Is.Not.Null);

            var transaction = transactionList!.FirstOrDefault();
            Assert.That(transaction, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(transaction?.Sku, Is.EqualTo("T2006"));
                Assert.That(transaction?.Amount, Is.EqualTo(11.20));
                Assert.That(transaction?.Currency, Is.EqualTo("EUR"));
            });
        }
    }
}