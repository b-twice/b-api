using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Budget.API.Models.Common;

namespace Budget.API.Models.Book
{
    [Table("BookStatuses")]
    public class BookStatus: AppKeywordLookup
    {
    }
}



