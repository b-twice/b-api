using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace b.Entities.Finance
{
  public class Expense
  {
    public string userName { get; set; }
    public string year { get; set; }
    public string month { get; set; }
    public string categoryName { get; set; }
    public int plannedExpense { get; set; }
    public int actualExpense { get; set; }
    public int difference
    {
      get
      {
        return plannedExpense - actualExpense;
      }
    }
  }
}




