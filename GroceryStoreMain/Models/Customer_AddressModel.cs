using System.ComponentModel.DataAnnotations;

namespace GroceryStoreMain.Models
{
    public class Customer_AddressModel
    {
        public Customer_AddressModel()
        {

        }
        public Customer_AddressModel(Customer_Address customer_Address)
        {
            this.ca_id = customer_Address.ca_id;
            this.address1 = customer_Address.address1;
            this.address2 = customer_Address.address2;
            this.address3 = customer_Address.address3;
            this.city = customer_Address.city;
            this.state = customer_Address.state;
            this.country = customer_Address.country;
            this.postal_code = customer_Address.postal_code;
        }

        public static Customer_Address GetCustomer_Address(Customer_AddressModel customer_AddressModel)
        {
            Customer_Address customer_Address = new Customer_Address();
            customer_Address.ca_id = customer_AddressModel.ca_id;
            customer_Address.address1 = customer_AddressModel.address1;
            customer_Address.address2 = customer_AddressModel.address2;
            customer_Address.address3 = customer_AddressModel.address3; 
            customer_Address.city = customer_AddressModel.city;
            customer_Address.state = customer_AddressModel.state;
            customer_Address.country = customer_AddressModel.country;
            customer_Address.postal_code = customer_AddressModel.postal_code;
            return customer_Address;
        }


        [Display(Name = "Customer Address ID")]
        public int ca_id { get; set; }

        [Required(ErrorMessage = "Address Line 1 is Required")]
        [Display(Name = "Address Line 1")]
        public string address1 { get; set; }

        [Required(ErrorMessage = "Address Line 2 is Required")]
        [Display(Name = "Address Line 2")]
        public string address2 { get; set; }

        [Required(ErrorMessage = "Address Line 3 is Required")]
        [Display(Name = "Address Line 3")]
        public string address3 { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required(ErrorMessage = "State is Required")]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Postal Code is Required")]
        [Display(Name = "Postal Code")]
        public string postal_code { get; set; }
    }
}