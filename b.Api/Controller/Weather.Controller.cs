using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace b.Api.Controller
{

  [Route("v1/analysis/weather")]
  [ApiController]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class WeatherController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly DarkSkyService _darkSkyService;
    public WeatherController(ILogger<WeatherController> logger, DarkSkyService darkSkyService)
    {
      _logger = logger;
      _darkSkyService = darkSkyService;
    }

    [HttpGet("forecast/today")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<DarkSkyResponse>> GetForecast([FromQuery] double latitude, [FromQuery] double longitude)
    {
      var optionalParameters = new OptionalParameters();
      optionalParameters.DataBlocksToExclude = new List<ExclusionBlocks>() { ExclusionBlocks.Flags, ExclusionBlocks.Alerts, ExclusionBlocks.Hourly, ExclusionBlocks.Minutely };
      return await _darkSkyService.GetForecast(latitude, longitude, optionalParameters);
    }

  }
}
