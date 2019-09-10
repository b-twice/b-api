
using System.ComponentModel.DataAnnotations;

namespace Budget.API.Models.Common
{
    public class AppLookup
    {

        public int id { get; set; }

        [Required]
        public string name { get; set; }

    }
}



