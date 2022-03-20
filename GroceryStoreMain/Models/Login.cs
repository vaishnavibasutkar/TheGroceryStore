using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreMain.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}