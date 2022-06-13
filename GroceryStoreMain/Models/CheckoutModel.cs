using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class CheckoutModel
    {
        public CustomerModel customer { get; set; }
        public Cart cart { get; set; }

        public decimal subtotal
        {
            get
            {
                decimal tot = 0;
                this.cart.Cart_Product_Assoc.ToList().ForEach(cpa=> { tot += cpa.Product.price; });
                return tot;

            }
        }
        public decimal shipping { get { return 0; } set { } }
        public decimal total { get { return subtotal + shipping; }  }
        public int testid { get; set; }
    }
}