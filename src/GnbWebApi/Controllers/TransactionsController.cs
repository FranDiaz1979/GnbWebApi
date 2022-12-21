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

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTransactionsController")]
        public async Task<IEnumerable<TransactionDto>> GetAll()
        {
            var list = await TransactionService.GetListAsysnc();
            _logger.LogInformation("{DateTime}: Transacciones consultadas.", DateTime.Now);
            return list;
        }

        [HttpGet(Name = "GetBySkuTransactionsController")]
        public async Task<TransactionTotalDto> GetBySku(string sku)
        {
            var result = await TransactionService.GetBySkuAsync(sku);
            _logger.LogInformation("{DateTime}: Sku consultado.", DateTime.Now);
            return result;
        }

    }
}