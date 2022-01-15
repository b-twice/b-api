using System.ComponentModel.DataAnnotations;

namespace B.API.Models
{
    public class AppKeywordLookup : AppLookup
    {

        public string Keyword { get; set; }

    }


    public class AppLookup : IAppLookup
    {

        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
    public interface IAppLookup
    {

        long Id { get; set; }

        string Name { get; set; }

    }
}



