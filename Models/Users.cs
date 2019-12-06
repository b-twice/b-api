﻿using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Users
    {
        public Users()
        {
            Earning = new HashSet<Earning>();
            TransactionRecord = new HashSet<TransactionRecord>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Earning> Earning { get; set; }
        public virtual ICollection<TransactionRecord> TransactionRecord { get; set; }
    }
}
