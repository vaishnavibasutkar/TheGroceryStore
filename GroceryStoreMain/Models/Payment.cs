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
    
    public partial class Payment
    {
        public int pay_id { get; set; }
        public Nullable<int> c_id { get; set; }
        public decimal amount { get; set; }
        public System.DateTime payment_date { get; set; }
        public Nullable<int> o_id { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
