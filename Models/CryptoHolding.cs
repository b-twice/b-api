using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("CryptoHolding")]
    public partial class CryptoHolding
    {
        public CryptoHolding()
        {
            CryptoSales = new HashSet<CryptoSale>();
        }

        [Key]
        public long Id { get; set; }
        public long CryptoCoinId { get; set; }
        [Required]
        public string PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Quantity { get; set; }

        [ForeignKey(nameof(CryptoCoinId))]
        [InverseProperty("CryptoHoldings")]
        public virtual CryptoCoin CryptoCoin { get; set; }
        [InverseProperty(nameof(CryptoSale.CryptoHolding))]
        public virtual ICollection<CryptoSale> CryptoSales { get; set; }
    }
}
