using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace b.Entities.Finance
{
  public class UserCategoryGrowth
  {
    public string fiscalYear { get; set; }
    public string userName { get; set; }
    public string categoryGroupName { get; set; }
    public string categoryName { get; set; }
    public int amount { get; set; }
    public int growth { get; set; }
  }
}



