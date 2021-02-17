using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace b.Entities.Food
{
  public class MealPlanRecipe
  {
    public int id { get; set; }
    public string mealPlanName { get; set; }
    public int mealPlanId { get; set; }
    public int recipeId { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public string cookbook { get; set; }
    public string url { get; set; }
    public int pageNumber { get; set; }
    public int servings { get; set; }
    public int count { get; set; }
    public decimal servingCost { get; set; }
    public decimal cost { get; set; }
    public decimal costOrganic { get; set; }
    public decimal costSeasonal { get; set; }

  }
}



