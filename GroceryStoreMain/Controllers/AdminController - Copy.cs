using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (Session["Username"] != null)
            {
                List<Product_Category> pc = context.Product_Category.ToList();
                List<ProductCategoryModel> pcList = new List<ProductCategoryModel>();
                pc.ForEach(c => pcList.Add(
                    new ProductCategoryModel()
                    {
                        pc_id = c.pc_id,
                        name = c.name,
                        description=c.description,
                        eff_start_dtm=c.eff_start_dtm,
                        eff_end_dtm=c.eff_end_dtm,
                        Products=c.Products
                    }
                    )) ;
                return View(pcList);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
         
        public ActionResult AddNewProductCategory()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        //[AntiForgeryToken]
        public ActionResult AddNewProductCategory(ProductCategoryModel product_Category)
        {
            if (Session["Username"] != null)
            {
                Product_Category pc = new Product_Category();
                pc.name = product_Category.name;
                pc.description = product_Category.description;
                pc.eff_end_dtm = product_Category.eff_end_dtm;
                pc.eff_start_dtm = DateTime.Now;
                context.Product_Category.Add(pc);
                context.SaveChanges();
                TempData["Message"] = "Added New Record in Product Categories.";
                return RedirectToAction("ProductCategories");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditProductCategory(int id)
        {
            if (Session["Username"] != null)
            {
                Product_Category pc_toedit = context.Product_Category.Where(p => p.pc_id == id).FirstOrDefault();
                ProductCategoryModel pcList = new ProductCategoryModel()
                {
                    pc_id = pc_toedit.pc_id,
                    name = pc_toedit.name,
                    description = pc_toedit.description,
                    eff_start_dtm = pc_toedit.eff_start_dtm,
                    eff_end_dtm = pc_toedit.eff_end_dtm,
                    Products = pc_toedit.Products
                };
                return View(pcList);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult EditProductCategory(Product_Category product_Category)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    context.Entry(product_Category).State = EntityState.Modified;
                    context.SaveChanges();
                    TempData["Message"] = "Edit for Product Category - " + product_Category.name + " is Successful.";
                }
                return RedirectToAction("ProductCategories");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult RemoveProductCategory(int id)
        {
            if (Session["Username"] != null)
            {
                Product_Category pc_todelete = context.Product_Category.Where(p => p.pc_id == id).FirstOrDefault();
                context.Product_Category.Remove(pc_todelete);
                context.SaveChanges();
                TempData["Message"] = "Product Category - " + pc_todelete.name + " is Deleted.";
                return RedirectToAction("ProductCategories");
            }
            else
            {   
                return RedirectToAction("Login");
            }
        }

        public ActionResult Products()
        {
            if (Session["Username"] != null)
            {
                List<Product> product = context.Products.ToList();
                return View(product);
            }
            else
            {   
                return RedirectToAction("Login");
            }
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