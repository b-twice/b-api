using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("CryptoSale")]
    public partial class CryptoSale
    {
        [Key]
        public long Id { get; set; }
        public long CryptoHoldingId { get; set; }
        [Required]
        public string SellDate { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        [ForeignKey(nameof(CryptoHoldingId))]
        [InverseProperty("CryptoSales")]
        public virtual CryptoHolding CryptoHolding { get; set; }
    }
}
