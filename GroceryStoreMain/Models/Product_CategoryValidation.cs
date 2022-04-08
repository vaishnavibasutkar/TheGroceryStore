using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreMain.Models
{
    public partial class DiscountModel
    {
        [Required(ErrorMessage = "Product Category Name is Required")] 
        public int dt_id { get; set; }

        [Display(Name = "Product ID")]
        [Required(ErrorMessage = "Product ID is Required")] 
        public int p_id { get; set; }

        [Required(ErrorMessage = "Product Discount Percentage is Required")]
        [Display(Name = "Product Discount Percentage")]
        public int discount_percentage { get; set; }

        [Display(Name = "Discount Start Date and Time")]
        [Required(ErrorMessage = "Discount Start Date and Time is Required")] 
        public System.DateTime eff_start_dtm { get; set; }

        [Display(Name = "Discount End Date and Time")]
        [Required(ErrorMessage = "Discount End Date and Time Required")] 
        public System.DateTime eff_end_dtm { get; set; }
        public virtual Product Product { get; set; }
    }

    //[ModelMetadataType(typeof(DiscountValidation))]
    //// Note the partial keyword
    //public partial class Discount { }
}