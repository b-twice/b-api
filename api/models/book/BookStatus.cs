using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using B.API.Models.Common;

namespace B.API.Models.Book
{
    [Table("BookStatuses")]
    public class BookStatus: AppKeywordLookup
    {
    }
}



