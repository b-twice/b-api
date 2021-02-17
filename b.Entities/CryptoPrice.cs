using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class CryptoPrice
    {
        public long Id { get; set; }
        public long CryptoCoinId { get; set; }
        public decimal Price { get; set; }
        public DateTime QueryDate { get; set; }

        public virtual CryptoCoin CryptoCoin { get; set; }
    }
}
