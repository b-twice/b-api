using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace B.API 
{

    [Route("org")]
    [Produces("text/plain")]
    [ApiController]
    public class OrgController: ControllerBase
    {
        private readonly ILogger _logger;

        public OrgController ( ILogger<OrgController> logger)
        {
            _logger = logger;
        }

        [HttpGet("publlic/{primary}/{secondary}/{name}")]
        public IActionResult GetSecondary(string primary, string secondary, string name) 
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "org", "public", $"{primary}", $"{secondary}", $"{name}.html");
            return PhysicalFile(file, "text/plain");
        }

        [HttpGet("public/{primary}/{name}")]
        public IActionResult GetPrimary(string primary, string name) 
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "org", "public", $"{primary}", $"{name}.html");
            return PhysicalFile(file, "text/plain");
        }

        [Authorize]
        [HttpGet("private/{primary}/{secondary}/{name}")]
        public IActionResult GetPrivateSecondary(string primary, string secondary, string name) 
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "org", "private", $"{primary}", $"{secondary}", $"{name}.html");
            return PhysicalFile(file, "text/plain");
        }

        [Authorize]
        [HttpGet("private/{primary}/{name}")]
        public IActionResult GetPrivatePrimary(string primary, string name) 
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "org", "private", $"{primary}", $"{name}.html");
            return PhysicalFile(file, "text/plain");
        }



   }

}
