using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStoreMain.Controllers
{
    public class CustomerController : Controller
    {
        GroceryStoreDBEntities context = new GroceryStoreDBEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            ViewBag.Category = context.Product_Category.ToList();
            return View();
        }

        public ActionResult ProductByProductCategory(int id = 0)
        {
            Product_Category product_Category = context.Product_Category.FirstOrDefault(pc => pc.pc_id == id);
            //product_Category.Products.ToList().ForEach(p => p.imagepath = Server.MapPath(p.imagepath));
            ViewBag.Category = context.Product_Category.ToList();
            return View(product_Category);
        }
        public ActionResult ProductDetails(int id = 0)
        {
            Product product = context.Products.FirstOrDefault(p => p.p_id == id);
            //product_Category.Products.ToList().ForEach(p => p.imagepath = Server.MapPath(p.imagepath));
            ViewBag.Category = context.Product_Category.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult AddToCart(int id = 0)
        {
            if (Session["Username"] != null)
            {
                Cart cart = new Cart();
                if (context.Carts.Where(c => c.cart_id != (int)Session["id"]).Any())
                {
                    cart.name = "Name";
                    cart.comment = "Comment";
                    context.Carts.Add(cart);
                   
                }
                context.SaveChanges();
                cart = context.Carts.Where(c => c.cart_id == (int)Session["id"]).FirstOrDefault();
                Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
                cart_Product_Assoc.cart_id = cart.cart_id;
                cart_Product_Assoc.p_id = id;
                context.Cart_Product_Assoc.Add(cart_Product_Assoc);
                context.SaveChanges();
                return Json(cart);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult Blank()
        {
            ViewBag.Category = context.Product_Category.ToList();
            return View();
        }
    }
}