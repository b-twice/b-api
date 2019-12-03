
using System.ComponentModel.DataAnnotations;

namespace B.API.Models.Common
{
    public class AppLookup
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}



