using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Keyless]
    public partial class LatestCryptoCoinPrice
    {
        public long? CryptoCoinId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string QueryDate { get; set; }
    }
}
