using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class DistributorModel
    {
        public DistributorModel()
        {

        }
        public DistributorModel(Distributor distributor)
        {
            this.d_id = distributor.d_id;
            this.company_name = distributor.company_name;
            this.address = distributor.address;
            this.city = distributor.city;
            this.postal_code = distributor.postal_code;
            this.email_id = distributor.email_id;
            this.phone_no = distributor.phone_no;
            this.username = distributor.username;
            this.password = distributor.password;
            this.password_lastmodified_dtm = distributor.password_lastmodified_dtm;
        }
        public static Distributor GetDistributor(DistributorModel distributorModel)
        {
            Distributor distributor = new Distributor();
            distributor.d_id = distributorModel.d_id;
            distributor.company_name = distributorModel.company_name;
            distributor.address = distributorModel.address;
            distributor.city = distributorModel.city;
            distributor.postal_code = distributorModel.postal_code;
            distributor.email_id = distributorModel.email_id;
            distributor.phone_no = distributorModel.phone_no;
            distributor.username = distributorModel.username;
            distributor.password = distributorModel.password;
            distributor.password_lastmodified_dtm = distributorModel.password_lastmodified_dtm;
            return distributor;

        }

        [Display(Name = "Distributor ID")]
        public int d_id { get; set; }

        [Required(ErrorMessage = "Company Name is Required")]
        [Display(Name = "Company Name")]
        public string company_name { get; set; }

        [Required(ErrorMessage = "Company Address is Required")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        //postalcode validatio left
        public Nullable<int> postal_code { get; set; }

        [Required(ErrorMessage = "Email ID is Required")]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string email_id { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public decimal phone_no { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Password Last Modified Date and Time")]
        public System.DateTime password_lastmodified_dtm { get; set; }
    }
}