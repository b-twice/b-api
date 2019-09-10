// using System.Threading.Tasks;
// using DarkSky.Models;
// using DarkSky.Services;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace Budget.API 
// {

//     [Route("Weather")]
//     [ApiController]
//     [ApiConventionType(typeof(DefaultApiConventions))]
//     public class WeatherController: ControllerBase
//     {
//         private readonly ILogger _logger;
//         private readonly DarkSkyService _darkSkyService;
//         public WeatherController(ILogger<WeatherController> logger, DarkSkyService darkSkyService)
//         {
//            _logger = logger;
//           _darkSkyService = darkSkyService;
//         }

//         [HttpGet("Forecast")]
//         [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
//         public async Task<ActionResult<DarkSkyResponse>> GetForecast() 
//         {
//             return await _darkSkyService.GetForecast(42.915, -78.741);

//         }

//    }
// }
