using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class User
    {

        public string Name {
            get 
            {
                return $"{FirstName} {LastName}";
            }
        
        }
    }
}
