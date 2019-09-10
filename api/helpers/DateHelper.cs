using System;

namespace Budget.API.Helpers
{
    class DateHelper {

        static public DateTime Parse (string date) 
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

        }

        static public string ParseYear (string date) 
        {
            return Parse(date).ToString("yyyy");

        }
        
        static public string ParseMonth (string date) 
        {
            return Parse(date).ToString("MM");

        }
    }

}
 