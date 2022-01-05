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
                .Include(r => r.RecipeIngredients).ThenInclude(i => i.FoodProduct);
        }

        public IQueryable<Recipe> Order(IQueryable<Recipe> recipes, string sortName) 
        {
            switch(sortName) {
                case "id_asc":
                    recipes = recipes.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    recipes = recipes.OrderByDescending(b => b.Id);
                    break;
                case "user_asc":
                    recipes = recipes.OrderBy(b => b.User.FirstName);
                    break;
                case "user_desc":
                    recipes = recipes.OrderByDescending(b => b.User.FirstName);
                    break;
                case "recipeCategory_asc":
                    recipes = recipes.OrderBy(b => b.RecipeCategory.Name);
                    break;
                case "recipeCategory_desc":
                    recipes = recipes.OrderByDescending(b => b.RecipeCategory.Name);
                    break;
                case "cookbook_asc":
                    recipes = recipes.OrderBy(b => b.Cookbook.Name);
                    break;
                case "cookbook_desc":
                    recipes = recipes.OrderByDescending(b => b.Cookbook.Name);
                    break;
                case "name_asc":
                    recipes = recipes.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    recipes = recipes.OrderByDescending(b => b.Name);
                    break;
                case "servings_asc":
                    recipes = recipes.OrderBy(b => b.Servings);
                    break;
                case "servings_desc":
                    recipes = recipes.OrderByDescending(b => b.Servings);
                    break;
                case "pageNumber_asc":
                    recipes = recipes.OrderBy(b => b.PageNumber);
                    break;
                case "pageNumber_desc":
                    recipes = recipes.OrderByDescending(b => b.PageNumber);
                    break;
                case "url_asc":
                    recipes = recipes.OrderBy(b => b.Url);
                    break;
                case "url_desc":
                    recipes = recipes.OrderByDescending(b => b.Url);
                    break;
              default:
                    break;
            }
            return recipes;
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
