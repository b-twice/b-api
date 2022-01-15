using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("CryptoCoin")]
    public partial class CryptoCoin : AppLookup
    {
        public CryptoCoin()
        {
            CryptoHoldings = new HashSet<CryptoHolding>();
            CryptoPrices = new HashSet<CryptoPrice>();
        }

        [InverseProperty(nameof(CryptoHolding.CryptoCoin))]
        public virtual ICollection<CryptoHolding> CryptoHoldings { get; set; }
        [InverseProperty(nameof(CryptoPrice.CryptoCoin))]
        public virtual ICollection<CryptoPrice> CryptoPrices { get; set; }
    }
}
