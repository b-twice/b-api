using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("CryptoPrice")]
    public partial class CryptoPrice
    {
        [Key]
        public long Id { get; set; }
        public long CryptoCoinId { get; set; }
        public long Price { get; set; }
        [Required]
        public string QueryDate { get; set; }

        [ForeignKey(nameof(CryptoCoinId))]
        [InverseProperty("CryptoPrices")]
        public virtual CryptoCoin CryptoCoin { get; set; }
    }
}
