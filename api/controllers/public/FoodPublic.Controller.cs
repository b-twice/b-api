using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Models;
using Budget.API.Entities;
using Budget.API.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace Budget.API 
{
    [Route("api/public/food/")]
    public class FoodPublicController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public FoodPublicController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("meal-plan/latest")]
        public IActionResult GetLatestMealPlan()
        {

            return Ok(_context.MealPlans.OrderByDescending(o => o.id).FirstOrDefault());
        }

        [HttpGet("meal-plan/recipes/{id}")]
        public IActionResult GetMealPlanRecipes (int id)
        {
            return Ok(
                _context.MealPlanRecipes.Where(o => o.mealPlanId == id)
            );
        }

        [HttpGet("recipes/{id}")]
        public IActionResult GetRecipe (int id)
        {
            return Ok(
                _context.MealPlanRecipes.FirstOrDefault(o => o.recipeId == id)
            );
        }

 
        [HttpGet("meal-plan/groceries/{id}")]
        public IActionResult GetMealPlanGroceries (int id)
        {
            return Ok(
                _context.MealPlanGroceries.Where(o => o.mealPlanId == id).OrderBy(o => o.name)
            );
        }

        [HttpGet("meal-plan/{id}")]
        public IActionResult GetMealPlan (int id)
        {
            return Ok(
                _context.MealPlans.FirstOrDefault(o => o.id == id)
            );
        }

        [HttpGet("meal-plan")]
        public IActionResult GetMealPlans()
        {
            return Ok(_context.MealPlans.OrderByDescending(o => o.id));
        }

        [HttpGet("recipes/{id}/ingredients")]
        public IActionResult GetRecipeIngredients(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(t => t.id == id);
            return Ok(_context.RecipeIngredients.Where(o => o.recipe == recipe.name));
        }

    }
}