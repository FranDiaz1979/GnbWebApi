using Castle.Core.Logging;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Interfaces;

namespace TestGnbWebApi.Controllers
{
    public class TransactionControllerTesting
    {
        private Microsoft.Extensions.Logging.ILogger<TransactionsController> logger;
        private ITransactionService transactionService;
        private TransactionsController transactionsController;

        [SetUp]
        public void SetUp()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            this.logger = mockRepository.Create<Microsoft.Extensions.Logging.ILogger<TransactionsController>>().Object;
            var mock = mockRepository.Create<ITransactionService>();
            IEnumerable<TransactionDto> transactionDtos = new List<TransactionDto>()
            {
                new TransactionDto(){
                    Sku="T2006",
                    Amount=(decimal)11.20,
                    Currency="EUR",
                },
            };
            mock.Setup(x => x.GetListAsysnc()).Returns(Task.FromResult(transactionDtos));
            this.transactionService = mock.Object;
            transactionsController = new TransactionsController(logger, transactionService);
        }

        [Test]
        public async Task Get_Ok()
        {
            var response = await transactionsController.Get();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            var value = ((OkObjectResult)response).Value;
            Assert.That(value, Is.Not.Null);
            Assert.That(value,Is.InstanceOf<IEnumerable<TransactionDto>>());

            var transactionDto = ((IEnumerable<TransactionDto>)value).FirstOrDefault();
            Assert.That(transactionDto, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(transactionDto.Sku, Is.EqualTo("T2006"));
                Assert.That(transactionDto.Amount, Is.EqualTo(11.20));
                Assert.That(transactionDto.Currency, Is.EqualTo("EUR"));
            });
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
