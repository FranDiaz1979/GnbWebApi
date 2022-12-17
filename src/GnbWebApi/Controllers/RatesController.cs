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

        public RatesController(ILogger<RatesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRatesController")]
        public async Task<IEnumerable<RateDto>> GetAll()
        {
            var rateList = await RateService.GetListAsysnc();
            _logger.LogInformation("{DateTime}: Ratios consultados.", DateTime.Now);           
            return rateList;
        }
    }
}