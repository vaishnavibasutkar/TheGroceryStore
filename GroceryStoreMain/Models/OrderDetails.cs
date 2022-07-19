using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public partial class Order
    {
        public  Customer Customer { get; set; }
        
        public Order GetOrder(Order o)
        {
            GroceryStoreDBEntities context = new GroceryStoreDBEntities();
            o.Customer = context.Customers.FirstOrDefault(c => c.c_id == o.c_id);
            return o;
        }
    }
}