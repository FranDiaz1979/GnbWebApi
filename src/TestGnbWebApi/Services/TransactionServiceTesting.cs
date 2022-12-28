namespace TestGnbWebApi.Services
{
    using global::Domain;
    using global::Services;
    using Infraestructure;
    using Moq;

    public class TransactionServiceTesting
    {
        private IApiClient _apiClient;
        private ITransactionService transactionService;

        [SetUp]
        public void Setup()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            this._apiClient = mockRepository.Create<IApiClient>().Object;

            this.transactionService = new TransactionService(_apiClient);
        }

        [Test]
        public void TestingWorking()
        {
            Assert.Pass();
        }

        [Test]
        public void GetListAsysnc_Ok()
        {
            var task = this.transactionService.GetListAsysnc();
            task.Wait();
            Assert.That(task, Is.Not.Null);

            var transactionList= task.Result;
            Assert.That(transactionList, Is.Not.Null);

            var firstTransaction = transactionList.First();
            Assert.Multiple(() =>
            {
                Assert.That(firstTransaction.Sku, Is.EqualTo("T2006"));
                Assert.That(firstTransaction.Amount, Is.EqualTo(10.05));
                Assert.That(firstTransaction.Currency, Is.EqualTo("USD"));
            });
        }

        [Test]
        public void GetBySkuAsync_Ok()
        {
            var task = this.transactionService.GetBySkuAsync("T2006");
            task.Wait();
            Assert.That(task, Is.Not.Null);

            var transactionTotal = task.Result;
            Assert.That(transactionTotal, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(transactionTotal.Sku, Is.EqualTo("T2006"));
                Assert.That(transactionTotal.Amount, Is.EqualTo(15.0268));
            });
        }
    }
}