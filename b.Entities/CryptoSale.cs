using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class CryptoSale
    {
        public long Id { get; set; }
        public long CryptoHoldingId { get; set; }
        public DateTime SellDate { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SaleValue { get; set; }

        public virtual CryptoHolding CryptoHolding { get; set; }
    }
}
