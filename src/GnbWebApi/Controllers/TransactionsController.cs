using Microsoft.AspNetCore.Mvc;
using Domain;
using Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService transactionService)
        {
            this._logger = logger;
            this._transactionService = transactionService;
        }

        [HttpGet(Name = "GetTransactionsController")]
        public async Task<IEnumerable<TransactionDto>> GetAll()
        {
            var list = await _transactionService.GetListAsysnc();
            _logger.LogInformation("{DateTime}: Transacciones consultadas.", DateTime.Now);
            return list;
        }

        [HttpGet(Name = "GetBySkuTransactionsController")]
        public async Task<TransactionTotalDto> GetBySku(string sku)
        {
            var result = await _transactionService.GetBySkuAsync(sku);
            _logger.LogInformation("{DateTime}: Sku consultado.", DateTime.Now);
            return result;
        }

    }
}