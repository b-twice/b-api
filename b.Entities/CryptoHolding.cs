using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class CryptoHolding
    {
        public CryptoHolding()
        {
            CryptoSales = new HashSet<CryptoSale>();
        }

        public long Id { get; set; }
        public long CryptoCoinId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchaseValue { get; set; }

        public virtual CryptoCoin CryptoCoin { get; set; }
        public virtual ICollection<CryptoSale> CryptoSales { get; set; }
    }
}
