using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
  public partial class VLatestCryptoCoinPrice
  {
    public long CryptoCoinId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime QueryDate { get; set; }
  }
}
