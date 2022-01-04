namespace B.API.Models
{
    public class AppKeywordLookup : AppLookup
    {

        public string Keyword { get; set; }

    }


    public class AppLookup : IAppLookup
    {

        public long Id { get; set; }

        public string Name { get; set; }

    }
    public interface IAppLookup
    {

        long Id { get; set; }

        string Name { get; set; }

    }
}



