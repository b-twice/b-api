using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API 
{

    [Route("Weather")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class WeatherController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly DarkSkyService _darkSkyService;
        public WeatherController(ILogger<WeatherController> logger, DarkSkyService darkSkyService)
        {
           _logger = logger;
          _darkSkyService = darkSkyService;
        }

        [HttpGet("Forecast")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public async Task<ActionResult<DarkSkyResponse>> GetForecast([FromQuery] double latitude, [FromQuery] double longitude) 
        {
            var optionalParameters= new DarkSkyService.OptionalParameters();
            optionalParameters.DataBlocksToExclude = new List<ExclusionBlock>(){ExclusionBlock.Flags, ExclusionBlock.Alerts, ExclusionBlock.Hourly, ExclusionBlock.Minutely};
            return await _darkSkyService.GetForecast(latitude, longitude, optionalParameters );
        }

   }
}
