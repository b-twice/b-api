using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
    public ActionResult<PaginatedResult<Post>> GetBlogPostPage(
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
      var posts = _postRepository.Filter(_context.Posts, title, description, groups, authenticate, star);
      posts = _postRepository.Include(_postRepository.Order(posts, sortName)).Where(p => !authenticated ? p.Authenticate != 1 : true);
      var paginatedList = PaginatedList<Post>.Create(posts, pageNumber, pageSize);
      return Ok(new PaginatedResult<Post>(paginatedList, paginatedList.TotalCount));
    }


    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<Post>> GetBlogPosts(
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
    public ActionResult<IEnumerable<PostGroup>> GetBlogPostGroups(
        [FromQuery] int size
    )
    {
      var authenticated = User.Identity.IsAuthenticated;
      var groups = _context.PostGroups.Include(pg => pg.Posts).AsNoTracking();
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
    public ActionResult<Post> GetBlogPost(long id)
    {
      return Ok(_postRepository.Find(id));
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Post> CreateBlogPost([FromBody] Post item)
    {
      // Without this EF Core will not bind the FK to these entities
      _context.Entry(item.PostGroup).State = EntityState.Unchanged;

      return Create<Post>(item, nameof(CreateBlogPost));
    }
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateBlogPost(long id, [FromBody] Post item)
    {
      item.PostGroupId = item?.PostGroup?.Id ?? default(int);

      return Update<Post>(id, item);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteBlogPost(long id)
    {
      return Delete<Post>(id);
    }

  }
}
