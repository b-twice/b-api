using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Geocoding.Microsoft;

namespace Budget.API 
{

    [Route("Geocode")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GeocodeController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly BingMapsGeocoder _geocoder;
        public GeocodeController(ILogger<WeatherController> logger, BingMapsGeocoder geocoder)
        {
          _logger = logger;
          _geocoder = geocoder;
        }

        [HttpGet("Reverse")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public async Task<IEnumerable<BingAddress>> Reverse([FromQuery] double latitude, [FromQuery] double longitude) 
        {
            return await _geocoder.ReverseGeocodeAsync(latitude, longitude);
        }

   }
}
