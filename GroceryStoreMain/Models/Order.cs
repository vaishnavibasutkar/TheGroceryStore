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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Order_Details = new HashSet<Order_Details>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int o_id { get; set; }
        public Nullable<int> c_id { get; set; }
        public Nullable<int> ca_id { get; set; }
        public Nullable<int> dts_id { get; set; }
        public Nullable<int> os_id { get; set; }
        public System.DateTime order_date { get; set; }
        public decimal delivery_charge { get; set; }
        public decimal tax { get; set; }
        public decimal total_amount { get; set; }
        public string paymentmode { get; set; }
    
        public virtual Customer_Address Customer_Address { get; set; }
        public virtual Delivery_Time_Slot Delivery_Time_Slot { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        public virtual Order_Status Order_Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
