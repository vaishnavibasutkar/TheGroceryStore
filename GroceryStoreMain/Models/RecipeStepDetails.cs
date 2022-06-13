using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class RecipeStepDetails
    {
        public int rs_id { get; set; }
        public int step_number { get; set; }
        public string instruction { get; set; }
        public Nullable<int> r_id { get; set; }
        public Nullable<int> p_id { get; set; }
        public string product_unit { get; set; }
        public Nullable<int> amount_req { get; set; }

        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}