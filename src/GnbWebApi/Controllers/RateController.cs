using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RateController : ControllerBase, IRateController
    {
        private readonly ILogger<RateController> _logger;
        private readonly IRateService _rateService;

        public RateController(ILogger<RateController> logger, IRateService rateService)
        {
            this._logger = logger;
            this._rateService = rateService;
        }

        [HttpGet]
        public void ThrowError()
        {
            throw new NotImplementedException("Error producido para probar el control de errores");
        }

        [HttpGet(Name = "GetRateController")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);
            var result = await _rateService.GetAllAsync();

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