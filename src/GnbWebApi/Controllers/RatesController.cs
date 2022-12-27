using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRateService _rateService;

        public RatesController(ILogger<RatesController> logger, IRateService rateService)
        {
            this._logger = logger;
            this._rateService = rateService;
        }

        //De esta forma swagger da más información, pero estamos acoplados al modelo
        //[HttpGet(Name = "GetRatesController")]
        //public async Task<IEnumerable<RateDto>> GetAll()
        //{
        //    _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);           
        //    return await _rateService.GetListAsync();
        //}

        [HttpGet(Name = "GetRatesController")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);
            return Ok(await _rateService.GetListAsync());
        }
    }
}