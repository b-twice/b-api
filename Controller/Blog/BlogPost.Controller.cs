using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

namespace B.API.Controller
{

  [Route("v1/blog/posts")]
  [ApiController]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class BlogPostController : AppControllerBase
  {
    private readonly BlogPostRepository _postRepository;
    private readonly AppDbContext _context;

    private readonly ILogger _logger;
    public BlogPostController(AppDbContext context, ILogger<BlogPostController> logger, BlogPostRepository postRepository) : base(context, logger)
    {
      _postRepository = postRepository;
      _context = context;
      _logger = logger;
    }



    [HttpGet("page")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<PaginatedResult<Post>> GetPage(
        [FromQuery] string sortName,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string title,
        [FromQuery] string description,
        [FromQuery] List<long> groups,
        [FromQuery] long? authenticate,
        [FromQuery] long? star
    )
    {
      var authenticated = User.Identity.IsAuthenticated;
      var posts = _postRepository.Filter(_context.Posts.AsNoTracking(), title, description, groups, authenticate, star);
      posts = _postRepository.Include(_postRepository.Order(posts, sortName)).Where(p => !authenticated ? p.Authenticate != 1 : true);
      var paginatedList = PaginatedList<Post>.Create(posts, pageNumber, pageSize);
      return Ok(new PaginatedResult<Post>(paginatedList, paginatedList.TotalCount));
    }


    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<Post>> GetAll(
        [FromQuery] int size
    )
    {
      var authenticated = User.Identity.IsAuthenticated;
      var posts = _postRepository.FindAll().Where(p => !authenticated ? p.Authenticate != 1 : true).OrderByDescending(b => b.Date);
      if (size > 0)
      {
        return Ok(posts.Take(size));
      }
      return Ok(posts);
    }

    [HttpGet("/groups")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<PostGroup>> GetGroups(
        [FromQuery] int size
    )
    {
      var authenticated = User.Identity.IsAuthenticated;
      var groups = _context.PostGroups.AsNoTracking().Include(pg => pg.Posts).AsNoTracking();
      if (!authenticated) {
        groups = groups.Where(g => g.Posts.Any(p => p.Authenticate == 0));
      }
      groups = groups.OrderBy(b => b.Name);
      if (size > 0)
      {
        return Ok(groups.Take(size));
      }
      return Ok(groups);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<Post> Get(long id)
    {
      return Ok(_postRepository.Find(id));
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Post> Create([FromBody] Post item)
    {
      return Create<Post>(item, nameof(Create), (long id ) => _postRepository.Find(id));
    }
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(200, Type = typeof(Post))]
   public ActionResult<Post> Update(long id, [FromBody] Post item)
    {
      return Update<Post>(id, item, (long id ) => _postRepository.Find(id));
    }
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      return Delete<Post>(id);
    }

  }
}
