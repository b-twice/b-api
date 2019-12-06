using System.ComponentModel.DataAnnotations;
using B.API.Models.Common;

namespace B.API.Models
{
    public class AppKeywordLookup: AppLookup
    {
        [Required]
        public string keyword { get; set; }
    }
}



