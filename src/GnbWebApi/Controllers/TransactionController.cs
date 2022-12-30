using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController : ControllerBase, ITransactionController
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            this._logger = logger;
            this._transactionService = transactionService;
        }

        [HttpGet(Name = "GetTransactionController")]
        public async Task<IActionResult> Get()
        {
            var list = await _transactionService.GetAllAsysnc();
            _logger.LogInformation("{DateTime}: Transacciones consultadas.", DateTime.Now);

            if (!list.Any())
            {
                return NoContent();
            }

            return Ok(list);
        }

        [HttpGet(Name = "GetBySkuTransactionController")]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var result = await _transactionService.GetBySkuAsync(sku);
            _logger.LogInformation("{DateTime}: Sku consultado.", DateTime.Now);

            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}