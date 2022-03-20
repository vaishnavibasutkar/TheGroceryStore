using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStoreMain.Models;

namespace GroceryStoreMain.Controllers
{
    public class AdminController : Controller
    {
        GroceryStoreDBEntities context = new GroceryStoreDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult ProductCategories()
        {
            List<Product_Category> pc = context.Product_Category.ToList();
            return View(pc);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var a= context.Admins.ToList();
            var result = context.Admins.Where(m => m.username == login.UserName && m.password == login.Password).FirstOrDefault();
            if (result != null)
            {
                ViewBag.Message = string.Format("Login Successfull");
                Session["Username"] = result.username;
                Session["Name"] = result.name;
                return RedirectToAction("Contact");
                //return RedirectToAction('EmployeeDetails', 'Employee);//I m returning to My Employee Action Method');
            }
            else
            {
                ViewBag.Message = string.Format("UserName and Password is incorrect");
                return View();
            }
        
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}