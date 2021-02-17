using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace b.Entities.Food
{
  public class FoodProduct
  {

    public int id { get; set; }

    public string name { get; set; }

    internal int foodUnitId { get; set; }
    public string unit
    {
      get
      {
        return foodUnitId == 1 ? "lb" : "oz";
      }
    }
    public int dirty { get; set; }
    public string measurement { get; set; }
    internal int quantityTypeId { get; set; }
    public string quantityType
    {
      get
      {
        switch (quantityTypeId)
        {
          case 1:
            return "Count";
          case 2:
            return "Weight";
          default:
            return "None";
        }
      }
    }
  }
}



