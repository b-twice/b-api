using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class RecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Recipe Find(long id) 
        {
            return IncludeAll(_context.Recipes.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<Recipe> FindAll() 
        {
            return Include(_context.Recipes).AsNoTracking();
        }

        public IQueryable<Recipe> Include(IQueryable<Recipe> recipes) 
        {
            return recipes.Include(b => b.User).Include(b => b.RecipeCategory).Include(b => b.Cookbook);
        }

        public IQueryable<Recipe> IncludeAll(IQueryable<Recipe> recipes) 
        {
            return recipes
                .Include(b => b.User)
                .Include(b => b.RecipeCategory)
                .Include(b => b.Cookbook)
                .Include(b => b.RecipeNotes)
                .Include(r => r.RecipeIngredients.OrderBy(i => i.FoodProduct.Name)).ThenInclude(i => i.FoodProduct);
        }

        public IQueryable<Recipe> Order(IQueryable<Recipe> items, string sortName) 
        {
            items = sortName switch 
            {
                "id_asc" => items.OrderBy(b => b.Id),
                "id_desc" => items.OrderByDescending(b => b.Id),
                "userId_asc" => items.OrderBy(b => b.User.FirstName),
                "userId_desc" => items.OrderByDescending(b => b.User.FirstName),
                "recipeCategoryId_asc" => items.OrderBy(b => b.RecipeCategory.Name),
                "recipeCategoryId_desc" => items.OrderByDescending(b => b.RecipeCategory.Name),
                "cookbookId_asc" => items.OrderBy(b => b.Cookbook.Name),
                "cookbookId_desc" => items.OrderByDescending(b => b.Cookbook.Name),
                "name_asc" => items.OrderBy(b => b.Name),
                "name_desc" => items.OrderByDescending(b => b.Name),
                "servings_asc" => items.OrderBy(b => b.Servings),
                "servings_desc" => items.OrderByDescending(b => b.Servings),
                "pageNumber_asc" => items.OrderBy(b => b.PageNumber),
                "pageNumber_desc" => items.OrderByDescending(b => b.PageNumber),
                "url_asc" => items.OrderBy(b => b.Url),
                "url_desc" => items.OrderByDescending(b => b.Url),
                "rating_asc" => items.OrderBy(b => b.Rating),
                "rating_desc" => items.OrderByDescending(b => b.Rating),
                "complexity_asc" => items.OrderBy(b => b.Complexity),
                "complexity_desc" => items.OrderByDescending(b => b.Complexity),
                "makeCount_asc" => items.OrderBy(b => b.MakeCount),
                "makeCount_desc" => items.OrderByDescending(b => b.MakeCount),
                "lastMade_asc" => items.OrderBy(b => b.LastMade),
                "lastMade_desc" => items.OrderByDescending(b => b.LastMade),
                 _ => items
            };
            return items;
        }
 
        public IQueryable<Recipe> Filter(IQueryable<Recipe> recipes, List<long> users, List<long> categories, List<long> cookbooks, List<long> products, string name)
        {
            if (users?.Any() == true) {
                recipes = recipes.Where(b => users.Contains(b.User.Id));
            }
            if (categories?.Any() == true) {
                recipes = recipes.Where(b => categories.Contains(b.RecipeCategory.Id));
            }
            if (cookbooks?.Any() == true) {
                recipes = recipes.Where(b => cookbooks.Contains(b.Cookbook.Id));
            }
            if (products?.Any() == true) {
                recipes = recipes.Where(b => b.RecipeIngredients.Any(r => products.Any(t => t == r.FoodProductId)));
            }
            if (!string.IsNullOrEmpty(name)) {
                recipes = recipes.Where(b => b.Name.ToLower().Contains(name.ToLower()));
            }
         
            return recipes;
        }

  }
}
