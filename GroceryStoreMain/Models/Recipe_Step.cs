//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStoreMain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipe_Step
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
