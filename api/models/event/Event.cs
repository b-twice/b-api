using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Event
{
    public class Event 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public bool complete{ get; set; }
        // public bool important { get; set; }
        // public string reoccuringType { get; set; }
        // public string location { get; set; }
        // public string url { get; set; }

        public EventUser eventUser { get; set; }
  }
}





