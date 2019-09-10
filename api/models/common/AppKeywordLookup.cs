using System.ComponentModel.DataAnnotations;
using Budget.API.Models.Common;

namespace Budget.API.Models.Book
{
    public class AppKeywordLookup: AppLookup
    {
        [Required]
        public string keyword { get; set; }
    }
}



