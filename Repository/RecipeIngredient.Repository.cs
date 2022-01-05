using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class RecipeIngredientRepository
    {
        private readonly AppDbContext _context;

        public RecipeIngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public RecipeIngredient Find(long id) 
        {
            return Include(_context.RecipeIngredients.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<RecipeIngredient> FindAll() 
        {
            return Include(_context.RecipeIngredients).AsNoTracking();
        }

        public IQueryable<RecipeIngredient> Include(IQueryable<RecipeIngredient> recipeIngredients) 
        {
            return recipeIngredients.Include(b => b.Recipe).Include(b => b.FoodProduct);
        }

        public IQueryable<RecipeIngredient> Order(IQueryable<RecipeIngredient> recipeIngredients, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case "id_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.Id);
                    break;
                case "count_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.Count);
                    break;
                case "count_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.Count);
                    break;
                case "weight_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.Weight);
                    break;
                case "weight_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.Weight);
                    break;
                case "measurement_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.Measurement);
                    break;
                case "measurement_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.Measurement);
                    break;
                case "foodProduct_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.FoodProduct.Name);
                    break;
                case "foodProduct_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.FoodProduct.Name);
                    break;
                case "recipe_asc":
                    recipeIngredients = recipeIngredients.OrderBy(b => b.Recipe.Name);
                    break;
                case "recipe_desc":
                    recipeIngredients = recipeIngredients.OrderByDescending(b => b.Recipe.Name);
                    break;
               default:
                    break;
            }
            return recipeIngredients;
        }
 
        public IQueryable<RecipeIngredient> Filter(IQueryable<RecipeIngredient> recipeIngredients, List<long> products, List<long> recipes)
        {
            if (recipes?.Any() == true) {
                recipeIngredients = recipeIngredients.Where(b => recipes.Contains(b.Recipe.Id));
            }
            if (products?.Any() == true) {
                recipeIngredients = recipeIngredients.Where(b => products.Contains(b.FoodProduct.Id));
            }
            return recipeIngredients;
        }

  }
}
