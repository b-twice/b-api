using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using B.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace B.API 
{

    [Route("v1/blog")]
    [Produces("text/plain")]
    [ApiController]
    public class BlogContentController: ControllerBase
    {
        private readonly ILogger _logger;

        private readonly AppDbContext _context;

        public BlogContentController (AppDbContext context, ILogger<BlogContentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{**content}")]
        public IActionResult GetPostContent(string primary, string name) 
        {
            var authenticated = User.Identity.IsAuthenticated;

            var path = HttpContext.Request.Path.Value.Replace("v1/blog/content/", "").TrimStart('/');

            var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Path == path && (!authenticated ? p.Authenticate != 1 : true));

            if (post == null) {
                return NotFound();
            }

            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "org", $"{post.Path}.html");
            return PhysicalFile(file, "text/plain");
        }

  }

}
