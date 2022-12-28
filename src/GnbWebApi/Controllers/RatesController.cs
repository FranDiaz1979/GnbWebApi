using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Services;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RatesController : ControllerBase, IRatesController
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRateService _rateService;

        public RatesController(ILogger<RatesController> logger, IRateService rateService)
        {
            this._logger = logger;
            this._rateService = rateService;
        }

        [HttpGet]
        public void ThrowError()
        {
            throw new Exception("Error producido para probar el control de errores");
        }

        [HttpGet(Name = "GetRatesController")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);
            var result = await _rateService.GetListAsync();

            if (!result.Any())
            {
                return NotFound();
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