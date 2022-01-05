using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class MealPlanNote
    {
        public long Id { get; set; }
        public long MealPlanId { get; set; }
        public string Content { get; set; }

        public virtual MealPlan MealPlan { get; set; }
    }
}
