using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace B.API.Models.Event
{
    public class EventUser 
    {
        public int id { get; set; }
        public string authId { get; set; }
        public string  name { get; set; }
  }
}





