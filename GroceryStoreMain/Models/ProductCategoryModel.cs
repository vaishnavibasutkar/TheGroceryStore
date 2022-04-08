using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreMain.Models
{
    public class ProductCategoryModel
    {
        public int pc_id { get; set; }
        [Required(ErrorMessage = "Product Category Name is Required")]
        [Display(Name = "Product Category Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Product Category Description is Required")]
        [Display(Name = "Product Category Description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Product Category Effective Start date is Required")]
        [Display(Name = "Product Category Effective Start Date")]
        public Nullable<System.DateTime> eff_start_dtm { get; set; }

        [Required(ErrorMessage = "Product Category Effective End Date is Required")]
        [Display(Name = "Product Category Effective End Date")]
        [DataType(DataType.DateTime,ErrorMessage ="Enter Valid Date and Time.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> eff_end_dtm { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}