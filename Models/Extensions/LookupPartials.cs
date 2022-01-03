namespace B.API.Models
{
    public partial class BookAuthor : IAppLookup {}
    public partial class BookCategory: IAppLookup {}
    public partial class BookStatus: IAppKeywordLookup{}
    public partial class CookbookAuthor : AppLookup {}
    public partial class Supermarket : AppLookup {}
    public partial class FoodCategory : AppLookup {}
    public partial class FoodQuantityType : AppLookup {}
    public partial class FoodUnit : AppLookup {}
    public partial class RecipeCategory : AppLookup {}
    public partial class Bank: IAppLookup{}
    public partial class TransactionCategory: IAppLookup{}
    public partial class TransactionTag: IAppLookup{}
}
