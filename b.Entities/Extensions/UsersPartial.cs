using System;
using System.Collections.Generic;

namespace b.Entities
{
  public partial class User
  {

    public string Name
    {
      get
      {
        return $"{FirstName} {LastName}";
      }

    }
  }
}
