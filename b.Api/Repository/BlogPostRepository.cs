using System.Linq;
using Microsoft.EntityFrameworkCore;
using b.Entities;
using System.Collections.Generic;

namespace b.Api.Database
{

  public class BlogPostRepository
  {
    private readonly AppDbContext _context;

    public BlogPostRepository(AppDbContext context)
    {
      _context = context;
    }

    public Post Find(long id)
    {
      return Include(_context.Posts.AsNoTracking()).First(b => b.Id == id);
    }


    public IQueryable<Post> FindAll()
    {
      return Include(_context.Posts).AsNoTracking();
    }

    public IQueryable<Post> Include(IQueryable<Post> posts)
    {
      return posts.Include(b => b.PostGroup);
    }

    public IQueryable<Post> Order(IQueryable<Post> posts, string sortName)
    {
      // TODO: tranlsate this to a generic method using IQueryable? 
      // May not be worth it, this is clear and conscise as is
      switch (sortName)
      {
        case "id_asc":
          posts = posts.OrderBy(b => b.Id);
          break;
        case "id_desc":
          posts = posts.OrderByDescending(b => b.Id);
          break;
        case "title_asc":
          posts = posts.OrderBy(b => b.Title);
          break;
        case "title_desc":
          posts = posts.OrderByDescending(b => b.Title);
          break;
        case "postGroup_asc":
          posts = posts.OrderBy(b => b.PostGroup.Name);
          break;
        case "postGroup_desc":
          posts = posts.OrderByDescending(b => b.PostGroup.Name);
          break;
        case "description_asc":
          posts = posts.OrderBy(b => b.Description);
          break;
        case "description_desc":
          posts = posts.OrderByDescending(b => b.Description);
          break;
        case "date_asc":
          posts = posts.OrderBy(b => b.Date);
          break;
        case "date_desc":
          posts = posts.OrderByDescending(b => b.Date);
          break;
        case "authenticate_asc":
          posts = posts.OrderBy(b => b.Authenticate);
          break;
        case "authenticate_desc":
          posts = posts.OrderByDescending(b => b.Authenticate);
          break;
        case "star_asc":
          posts = posts.OrderBy(b => b.Star);
          break;
        case "star_desc":
          posts = posts.OrderByDescending(b => b.Star);
          break;
        case "path_asc":
          posts = posts.OrderBy(b => b.Path);
          break;
        case "path_desc":
          posts = posts.OrderByDescending(b => b.Path);
          break;
        default:
          break;
      }
      return posts;
    }

    public IQueryable<Post> Filter(IQueryable<Post> posts, string title, string description, List<long> groups, long? authenticate, long? star)
    {
      if (!string.IsNullOrEmpty(description))
      {
        posts = posts.Where(b => b.Description.ToLower().Contains(description.ToLower()));
      }
      if (!string.IsNullOrEmpty(title))
      {
        posts = posts.Where(b => b.Title.ToLower().Contains(title.ToLower()));
      }
      if (authenticate.HasValue)
      {
        posts = posts.Where(b => b.Authenticate == authenticate);
      }
      if (star.HasValue)
      {
        posts = posts.Where(b => b.Star == star);
      }
      if (groups?.Any() == true)
      {
        posts = posts.Where(b => groups.Contains(b.PostGroup.Id));
      }
      return posts;
    }

  }
}
