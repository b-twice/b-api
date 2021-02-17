using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace b.Entities.Finance
{
  public class Transaction
  {
    public int id { get; set; }
    public string userName { get; set; }
    public string bankName { get; set; }
    public string categoryGroupName { get; set; }
    public string categoryName { get; set; }
    public string date { get; set; }
    public string description { get; set; }
    public int amount { get; set; }
    public int notable { get; set; }
  }
}




