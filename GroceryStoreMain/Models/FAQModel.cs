using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class FAQModel
    {
        [Display(Name = "FAQ ID")]
        public int faq_id { get; set; }

        [Required(ErrorMessage = "Question is Required")]
        [Display(Name = "Question")]
        public string question { get; set; }

        [Required(ErrorMessage = "Answer is Required")]
        [Display(Name = "Answer")]
        public string answer { get; set; }

        [Display(Name = "Created Date and Time")]
        public System.DateTime created_dtm { get; set; }
    }
}