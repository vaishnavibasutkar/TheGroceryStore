using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Id is Required")]
        [Display(Name = "ID")]
        public string id { 
            get {
                return  _id;
            }
            set {
                _id = usertype+ " - "+value;
            } 
        }
        private string _id;
        
        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email ID is Required")]
        [Display(Name = "Email ID")]
        public string email { get; set; }

        [Required(ErrorMessage = "UserType is Required")]
        [Display(Name = "User Type")]
        public UserTypeEnum usertype {
            get {
                return (UserTypeEnum)_userTypeEnum;
            }
            set {
                _userTypeEnum = value;
            } 
        }
        private UserTypeEnum _userTypeEnum;
    }
}