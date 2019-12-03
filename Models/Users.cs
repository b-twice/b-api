using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Users
    {
        public Users()
        {
            Earnings = new HashSet<Earnings>();
            Transactions = new HashSet<Transactions>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Earnings> Earnings { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
