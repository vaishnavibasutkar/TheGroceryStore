using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class ProductModel
    {
        public int p_id { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Product Description is Required")]
        [Display(Name = "Product Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Product Category ID is Required")]
        [Display(Name = "Product Category ID")]
        public int pc_id { get; set; }

        
        public virtual Product_Category Product_Category { get; set; }

        [Required(ErrorMessage = "Unit of Measurement is Required")]
        [Display(Name = "Unit of Measurement")]
        public int uom_id { get; set; }

        public virtual Unit_Of_Measurement Unit_Of_Measurement { get; set; }

        [Required(ErrorMessage ="Distributor ID is Required")]
        [Display(Name ="Distributor ID")]
        public int d_id { get; set; }
        public virtual Distributor Distributor { get; set; }

        [Required(ErrorMessage ="Price is Required")]
        [Display(Name ="Price")]
        public decimal price { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public string imagepath { get; set; }

    }
}