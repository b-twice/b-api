using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
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

        public IQueryable<Post> Order(IQueryable<Post> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(e => e.Id),
                "id_desc" => items.OrderByDescending(e => e.Id),
                "title_asc" => items.OrderBy(e => e.Title),
                "title_desc" => items.OrderByDescending(e => e.Title),
                "postGroupId_asc" => items.OrderBy(e => e.PostGroup.Name),
                "postGroupId_desc" => items.OrderByDescending(e => e.PostGroup.Name),
                "description_asc" => items.OrderBy(e => e.Description),
                "description_desc" => items.OrderByDescending(e => e.Description),
                "date_asc" => items.OrderBy(e => e.Date),
                "date_desc" => items.OrderByDescending(e => e.Date),
                "star_asc" => items.OrderBy(e => e.Star),
                "star_desc" => items.OrderByDescending(e => e.Star),
                "path_asc" => items.OrderBy(e => e.Path),
                "path_desc" => items.OrderByDescending(e => e.Path),
               _ => items
            };
            return items;
       }
 
        public IQueryable<Post> Filter(IQueryable<Post> posts, string title, string description, List<long> groups, long? authenticate, long? star) 
        {
          if (!string.IsNullOrEmpty(description)) {
              posts = posts.Where(b => b.Description.ToLower().Contains(description.ToLower()));
          }
          if (!string.IsNullOrEmpty(title)) {
              posts = posts.Where(b => b.Title.ToLower().Contains(title.ToLower()));
          }
          if (authenticate.HasValue) {
              posts = posts.Where(b => b.Authenticate == authenticate);
          }
          if (star.HasValue) {
              posts = posts.Where(b => b.Star  == star);
          }
          if (groups?.Any() == true) {
              posts = posts.Where(b => groups.Contains(b.PostGroup.Id));
          }
          return posts;
        }

  }
}
