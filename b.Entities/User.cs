using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class User
    {
        public User()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
