using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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

        #region Product Category - Admin
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
                        description = c.description,
                        eff_start_dtm = c.eff_start_dtm,
                        eff_end_dtm = c.eff_end_dtm,
                        Products = c.Products
                    }
                    ));
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
                    eff_start_dtm = (System.DateTime)pc_toedit.eff_start_dtm,
                    eff_end_dtm = (System.DateTime)pc_toedit.eff_end_dtm,
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

        #endregion

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

        #region FAQ - Admin
        public ActionResult FAQ()
        {
            if (Session["Username"] != null)
            {
                List<FAQ> fAQs = context.FAQs.ToList();
                List<FAQModel> faqList = new List<FAQModel>();
                fAQs.ForEach(f => faqList.Add(
                    new FAQModel()
                    {
                        faq_id = f.faq_id,
                        question = f.question,
                        answer = f.answer,
                        created_dtm = f.created_dtm
                    }
                    ));
                return View(faqList);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddNewFAQ()
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
        public ActionResult AddNewFAQ(FAQModel faq)
        {
            if (Session["Username"] != null)
            {
                FAQ fAQ = new FAQ();
                fAQ.question = faq.question;
                fAQ.answer = faq.answer;
                fAQ.created_dtm = DateTime.Now;
                context.FAQs.Add(fAQ);
                context.SaveChanges();
                TempData["Message"] = "Added New Record in FAQ.";
                return RedirectToAction("FAQ");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditFAQ(int id)
        {
            if (Session["Username"] != null)
            {
                FAQ faq_toedit = context.FAQs.Where(f => f.faq_id == id).FirstOrDefault();
                FAQModel faq = new FAQModel()
                {
                    faq_id = faq_toedit.faq_id,
                    question = faq_toedit.question,
                    answer = faq_toedit.answer,
                    created_dtm = faq_toedit.created_dtm
                };

                //ProductCategoryModel pcList = new ProductCategoryModel()
                //{
                //    pc_id = pc_toedit.pc_id,
                //    name = pc_toedit.name,
                //    description = pc_toedit.description,
                //    eff_start_dtm = (System.DateTime)pc_toedit.eff_start_dtm,
                //    eff_end_dtm = (System.DateTime)pc_toedit.eff_end_dtm,
                //    Products = pc_toedit.Products
                //};
                return View(faq);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult EditFAQ(FAQ faq)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    context.Entry(faq).State = EntityState.Modified;
                    context.SaveChanges();
                    TempData["Message"] = "FAQ - " + faq.faq_id + " is Updated.";
                }
                return RedirectToAction("FAQ");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult RemoveFAQ(int id)
        {
            if (Session["Username"] != null)
            {
                FAQ faq_todelete = context.FAQs.Where(f => f.faq_id == id).FirstOrDefault();
                context.FAQs.Remove(faq_todelete);
                context.SaveChanges();
                TempData["Message"] = "FAQ - " + faq_todelete.faq_id + " is Deleted.";
                return RedirectToAction("FAQ");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        #endregion

        #region User - Admin
        public ActionResult Users()
        {
            if (Session["Username"] != null)
            {
                List<Users> users = new List<Users>();
                this.GetAdmins(out List<Users> admins);
                users.AddRange(admins);

                this.GetCustomers(out List<Users> customers);
                users.AddRange(customers);

                this.GetDistributors(out List<Users> distributors);
                users.AddRange(distributors);

                return View(users);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        private void GetDistributors(out List<Users> distributors)
        {
            List<Users> users = new List<Users>();
            context.Distributors.ToList().ForEach(d =>
            {
                users.Add(new Users()
                {

                    email = d.email_id,
                    username = d.username,
                    usertype = UserTypeEnum.Distributor,
                    id = d.d_id.ToString(),
                });
            });
            distributors = users;
        }

        private void GetCustomers(out List<Users> customers)
        {
            List<Users> users = new List<Users>();
            context.Customers.ToList().ForEach(c =>
            {
                users.Add(new Users()
                {
                    email = c.email_id,
                    username = c.email_id,
                    usertype = UserTypeEnum.Customer,
                    id = c.c_id.ToString()
                });
            });
            customers = users;
        }

        public void GetAdmins(out List<Users> admins)
        {
            List<Users> users = new List<Users>();

            context.Admins.ToList().ForEach(a =>
            {
                users.Add(new Users()
                {
                    email = "",
                    username = a.username,
                    usertype = UserTypeEnum.Admin,
                    id = a.a_id.ToString()
                });
            });

            admins = users;
        }

        //public ActionResult AddNewFAQ()
        //{
        //    if (Session["Username"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        //[HttpPost]
        ////[AntiForgeryToken]
        //public ActionResult AddNewFAQ(FAQModel faq)
        //{
        //    if (Session["Username"] != null)
        //    {
        //        FAQ fAQ = new FAQ();
        //        fAQ.question = faq.question;
        //        fAQ.answer = faq.answer;
        //        fAQ.created_dtm = DateTime.Now;
        //        context.FAQs.Add(fAQ);
        //        context.SaveChanges();
        //        TempData["Message"] = "Added New Record in FAQ.";
        //        return RedirectToAction("FAQ");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        public ActionResult EditUser(string id)
        {
            if (Session["Username"] != null)
            {
                string viewName = "";
                object modelName = null;
                var userdetail = id.Split('-');
                int user_id = Convert.ToInt32(userdetail[1].Trim());
                switch (userdetail[0].Trim())
                {
                    case nameof(UserTypeEnum.Admin):
                        viewName = "AdminEditUser";
                        modelName = context.Admins.Where(a => a.a_id == user_id).FirstOrDefault();
                        break;

                    case nameof(UserTypeEnum.Customer):
                        viewName = "CustomerEditUser";
                        modelName = context.Customers.Where(c => c.c_id == user_id).FirstOrDefault();
                        break;

                    case nameof(UserTypeEnum.Distributor):
                        viewName = "DistributorEditUser";
                        modelName = context.Distributors.Where(d => d.d_id == user_id).FirstOrDefault();
                        break;

                    default:
                        break;
                }
                return View(viewName, modelName);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public ActionResult AdminEditUser(Admin admin)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    context.Entry(admin).State = EntityState.Modified;
                    context.SaveChanges();
                    TempData["Message"] = "Admin - " + admin.name + " is Updated.";
                }
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //[HttpPost]
        //public ActionResult EditFAQ(FAQ faq)
        //{
        //    if (Session["Username"] != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            context.Entry(faq).State = EntityState.Modified;
        //            context.SaveChanges();
        //            TempData["Message"] = "FAQ - " + faq.faq_id + " is Updated.";
        //        }
        //        return RedirectToAction("FAQ");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        public ActionResult RemoveUser(string id)
        {
            if (Session["Username"] != null)
            {
                var userdetail = id.Split('-');
                int user_id = Convert.ToInt32(userdetail[1].Trim());
                switch (userdetail[0].Trim())
                {
                    case nameof(UserTypeEnum.Admin):
                        context.Admins.ToList().RemoveAll(a => a.a_id == user_id);
                        break;
                    case nameof(UserTypeEnum.Customer):
                        context.Customers.ToList().RemoveAll(c => c.c_id == user_id);
                        break;
                    case nameof(UserTypeEnum.Distributor):
                        context.Distributors.ToList().RemoveAll(d => d.d_id == user_id);
                        break;
                    default:
                        break;
                }
                context.SaveChanges();
                TempData["Message"] = "User Deleted.";
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        #endregion

        #region Login - Logout - Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var a = context.Admins.ToList();
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

        #endregion

    }
}