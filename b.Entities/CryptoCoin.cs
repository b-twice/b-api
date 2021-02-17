using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class CryptoCoin
    {
        public CryptoCoin()
        {
            CryptoHoldings = new HashSet<CryptoHolding>();
            CryptoPrices = new HashSet<CryptoPrice>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CryptoHolding> CryptoHoldings { get; set; }
        public virtual ICollection<CryptoPrice> CryptoPrices { get; set; }
    }
}
