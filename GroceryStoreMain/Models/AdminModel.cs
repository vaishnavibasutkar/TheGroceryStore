using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class AdminModel
    {
        public AdminModel()
        {

        }
        public AdminModel(Admin admin)
        {
            this.a_id = admin.a_id;
            this.username = admin.username;
            this.password = admin.password;
            this.name = admin.name;
            this.password_lastmodified_dtm = admin.password_lastmodified_dtm;
        }
        //public AdminModel(int aid, string username, string password, string name, DateTime password_lastmodified)
        //{
        //    this.a_id = aid;
        //    this.username = username;
        //    this.password = password;
        //    this.name = name;
        //    this.password_lastmodified_dtm = password_lastmodified;
        //}

        public static Admin GetAdmin(AdminModel adminModel)
        {
            Admin admin = new Admin();
            admin.a_id = adminModel.a_id;
            admin.username = adminModel.username;
            admin.password = adminModel.password;
            admin.name = adminModel.name;
            admin.password_lastmodified_dtm = adminModel.password_lastmodified_dtm;
            return admin;
        }


        [Display(Name = "Admin ID")]
        public int a_id { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name")]

        public string username { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        public string name { get; set; }


        [Display(Name = "Password Last Modified Date and Time Name")]

        public System.DateTime password_lastmodified_dtm { get; set; }

        //public static explicit operator AdminModel(Admin v)
        //{
            
        //}
    }
}