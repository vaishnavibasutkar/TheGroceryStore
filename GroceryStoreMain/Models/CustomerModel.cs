using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
                
        }
        public CustomerModel(Customer customer)
        {
            this.c_id = customer.c_id;
            this.full_name = customer.full_name;
            this.mobile_no = customer.mobile_no;
            this.alternate_mobile_no = customer.alternate_mobile_no;
            this.email_id = customer.email_id;
            this.ca_id = customer.ca_id;
            Customer_AddressModel customer_AddressModel = new Customer_AddressModel(customer.Customer_Address);
            this.Customer_Address = customer_AddressModel;

        }

        public static Customer GetCustomer(CustomerModel customerModel)
        {
            Customer customer = new Customer();
            customer.c_id = customerModel.c_id;
            customer.full_name = customerModel.full_name;
            customer.mobile_no = customerModel.mobile_no;
            customer.alternate_mobile_no = customerModel.alternate_mobile_no;
            customer.email_id = customerModel.email_id;
            customer.ca_id = customerModel.ca_id;
            return customer;

        }

        [Display(Name = "Customer ID")]
        public int c_id { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]

        public string full_name { get; set; }

        [Required(ErrorMessage = "Mobile No is Required")]
        [Display(Name = "Mobile Number")]
        public decimal mobile_no { get; set; }

        [Display(Name = "Alternate Mobile Number")]
        public Nullable<decimal> alternate_mobile_no { get; set; }


        [Required(ErrorMessage = "Email ID is Required")]
        [Display(Name = "Email ID")]
        public string email_id { get; set; }

        [Display(Name = "Customer Address ID")]
        public Nullable<int> ca_id { get; set; }

        public  Customer_AddressModel Customer_Address { get; set; }

        
    }
}