using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStoreMain.Controllers
{
    public class DistributorController : Controller
    {

        GroceryStoreDBEntities context = new GroceryStoreDBEntities();
        public ActionResult Index()
        {
            return View();
        }

    

        

        #region Product - Admin
        public ActionResult Products()
        {
            if (Session["Username"] != null)
            {
                List<Product> product = context.Products.ToList();
                List<ProductModel> productModels = new List<ProductModel>();
                product.ForEach(p =>
                {
                    productModels.Add(new ProductModel()
                    {
                        p_id = p.p_id,
                        name = p.name,
                        description = p.description,
                        uom_id = (int)p.uom_id,
                        Unit_Of_Measurement = p.Unit_Of_Measurement,
                        pc_id = (int)p.pc_id,
                        Product_Category = p.Product_Category,
                        d_id = (int)p.d_id,
                        Distributor = p.Distributor,
                        price = p.price
                    });
                });
                return View(productModels);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddNewProduct()
        {
            if (Session["Username"] != null)
            {
                ViewBag.ProductCategory = this.context.Product_Category.ToList();
                ViewBag.Distributor = this.context.Distributors.ToList();
                ViewBag.UOM = this.context.Unit_Of_Measurement.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        //[AntiForgeryToken]   
        public ActionResult AddNewProduct(ProductModel productModel)
        {
            if (Session["Username"] != null)
            {
                Product product = new Product();
                product.name = productModel.name;
                product.description = productModel.description;
                product.pc_id = productModel.pc_id;
                product.uom_id = productModel.uom_id;
                product.d_id = productModel.d_id;
                product.price = productModel.price;
                context.Products.Add(product);
                context.SaveChanges();
                TempData["Message"] = "Added New Record in Product.";
                return RedirectToAction("Products");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["Username"] != null)
            {
                Product product_toedit = context.Products.Where(p => p.p_id == id).FirstOrDefault();
                ProductModel productModel = new ProductModel()
                {
                    p_id = product_toedit.p_id,
                    name = product_toedit.name,
                    description = product_toedit.description,
                    price = product_toedit.price,
                    d_id = (int)product_toedit.d_id,
                    uom_id = (int)product_toedit.uom_id,
                    pc_id = (int)product_toedit.pc_id
                };
                ViewBag.ProductCategory = this.context.Product_Category.ToList();
                ViewBag.Distributor = this.context.Distributors.ToList();
                ViewBag.UOM = this.context.Unit_Of_Measurement.ToList();

                return View(productModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModel productModel)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    Product product = context.Products.Where(p => p.p_id == productModel.p_id).FirstOrDefault();

                    product.p_id = productModel.p_id;
                    product.name = productModel.name;
                    product.description = productModel.description;
                    product.uom_id = productModel.uom_id;
                    product.pc_id = productModel.pc_id;
                    product.d_id = productModel.d_id;
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                    TempData["Message"] = "Edit for Product - " + productModel.name + " is Successful.";
                }
                return RedirectToAction("Products");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult RemoveProduct(int id)
        {
            if (Session["Username"] != null)
            {
                Product p_todelete = context.Products.Where(p => p.p_id == id).FirstOrDefault();
                context.Products.Remove(p_todelete);
                context.SaveChanges();
                TempData["Message"] = "Product - " + p_todelete.name + " is Deleted.";
                return RedirectToAction("Products");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #endregion

        #region Order - Order Details

        public ActionResult OrderList()
        {
            if (Session["Username"] != null)
            {
                var orders = context.Orders.ToList();
                ViewBag.OrderStatus = this.context.Order_Status.ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult OrderList(IEnumerable<Order> orders)
        {
            if (Session["Username"] != null)
            {

                //var _order = context.Orders.FirstOrDefault(o => o.o_id == order.o_id);

                //_order.os_id = order.os_id;


                //context.Entry(_order).State = EntityState.Modified;
                //context.SaveChanges();
                return RedirectToAction("OrderList");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ViewOrderDetails(int id)
        {
            if (Session["Username"] != null)
            {
                ViewBag.OrderStatus = this.context.Order_Status.ToList();
                var order = context.Orders.FirstOrDefault(o => o.o_id == id);

                return View(order);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult ViewOrderDetails(Order order)
        {
            if (Session["Username"] != null)
            {

                var _order = context.Orders.FirstOrDefault(o => o.o_id == order.o_id);

                _order.os_id = order.os_id;


                context.Entry(_order).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("OrderList");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ChangeOrderStatus(int i, int os_id)
        {
            if (Session["Username"] != null)
            {
                var _order = context.Orders.FirstOrDefault();

                _order.os_id = os_id;


                context.Entry(_order).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("OrderList");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        #endregion

        #region Discount - Admin
        public ActionResult ManageDiscount()
        {
            if (Session["Username"] != null)
            {
                List<Discount> discount = context.Discounts.ToList();

                List<DiscountModel> dList = new List<DiscountModel>();
                discount.ForEach(d => dList.Add(
                    new DiscountModel()
                    {
                        dt_id = d.dt_id,
                        p_id = (int)d.p_id,
                        discount_percentage = d.discount_percentage,
                        eff_start_dtm = (DateTime)d.eff_start_dtm,
                        eff_end_dtm = (DateTime)d.eff_end_dtm,
                        Product = d.Product
                    }
                    ));
                return View(dList);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CreateNewDiscount()
        {

            if (Session["Username"] != null)
            {
                ViewBag.Product = context.Products.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewDiscount(DiscountModel productModel)
        {
            if (Session["Username"] != null)
            {
                //context.Entry(productModel).State = EntityState.Added;
                Discount d = new Discount()
                {
                    p_id = productModel.p_id,
                    discount_percentage = productModel.discount_percentage,
                    eff_start_dtm = productModel.eff_start_dtm,
                    eff_end_dtm = productModel.eff_end_dtm
                };
                context.Discounts.Add(d);
                context.SaveChanges();
                TempData["Message"] = "Added New Record in Discount.";
                return RedirectToAction("ManageDiscount");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult RemoveDiscount(int id)
        {
            if (Session["Username"] != null)
            {
                Discount discount_todelete = context.Discounts.Where(p => p.dt_id == id).FirstOrDefault();
                context.Discounts.Remove(discount_todelete);
                context.SaveChanges();
                TempData["Message"] = "Discount - " + discount_todelete.dt_id + " is Deleted.";
                return RedirectToAction("ManageDiscount");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #endregion

        #region Contact developer
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Layout = "~/Views/Shared/_DistributorLayoutPage.cshtml";
            return View();
        } 
        #endregion

        #region Login - Logout - Admin
        public ActionResult Login()
        {
            ViewBag.Title = "The Grocery Store - Distributor";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {

                var a = context.Distributors.ToList();
                var result = context.Distributors.Where(m => m.username == login.UserName && m.password == login.Password).FirstOrDefault();
                if (result != null)
                {
                    ViewBag.Message = string.Format("Login Successfull");
                    Session["Username"] = result.username;
                    Session["Name"] = result.company_name;
                    return RedirectToAction("Contact");
                    //return RedirectToAction('EmployeeDetails', 'Employee);//I m returning to My Employee Action Method');
                }
                else
                {
                    ViewBag.Message = string.Format("UserName and Password is incorrect");

                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        #endregion
    }
}