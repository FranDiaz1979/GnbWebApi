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

        [HttpGet(Name = "GetRatesController")]
        public async Task<IEnumerable<RateDto>> GetAll()
        {
            var rateList = await _rateService.GetListAsync();
            _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);           
            return rateList;
        }
    }
}