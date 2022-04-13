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
                ViewBag.Layout = "~/Views/Shared/_AdminLayoutPage.cshtml";
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
                    id = d.d_id
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
                    id = c.c_id
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
                    id = a.a_id
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

        //public ActionResult EditUser(string id)
        //{
        //    if (Session["Username"] != null)
        //    {
        //        string viewName = "";
        //        object modelName = null;
        //        var userdetail = id.Split('-');
        //        int user_id = Convert.ToInt32(userdetail[1].Trim());
        //        switch (userdetail[0].Trim())
        //        {
        //            case nameof(UserTypeEnum.Admin):
        //                viewName = "AdminEditUser";
        //                modelName = context.Admins.Where(a => a.a_id == user_id).FirstOrDefault();
        //                break;

        //            case nameof(UserTypeEnum.Customer):
        //                viewName = "CustomerEditUser";
        //                modelName = context.Customers.Where(c => c.c_id == user_id).FirstOrDefault();
        //                break;

        //            case nameof(UserTypeEnum.Distributor):
        //                viewName = "DistributorEditUser";
        //                modelName = context.Distributors.Where(d => d.d_id == user_id).FirstOrDefault();
        //                break;

        //            default:
        //                break;
        //        }
        //        return View(viewName, modelName);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}


        public ActionResult AdminAddEditUser(int id)
        {
            if (Session["Username"] != null)
            {
                if (Convert.ToInt32(id) == 0)
                {
                    return View(new AdminModel() { a_id = id });
                }
                else
                {
                    //var userdetail = id.Split('-');
                    //int user_id = Convert.ToInt32(userdetail[1].Trim());
                    Admin admin = context.Admins.Where(a => a.a_id == id).FirstOrDefault();
                    AdminModel adminModel = new AdminModel(admin);
                    return View(adminModel);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult AdminAddEditUser(AdminModel adminModel)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (adminModel.a_id == 0)
                    {

                        Admin admin = AdminModel.GetAdmin(adminModel);
                        admin.password_lastmodified_dtm = DateTime.Now;
                        context.Admins.Add(admin);
                        TempData["Message"] = "Admin - " + adminModel.name + " Added.";
                    }
                    else
                    {
                        Admin admin = AdminModel.GetAdmin(adminModel);
                        //check password change
                        context.Entry(admin).State = EntityState.Modified;
                        TempData["Message"] = "Admin - " + adminModel.name + " is Updated.";
                    }
                    context.SaveChanges();

                }
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult DistributorAddEditUser(int id)
        {
            if (Session["Username"] != null)
            {
                if (Convert.ToInt32(id) == 0)
                {
                    return View(new DistributorModel() { d_id = id });
                }
                else
                {
                    //var userdetail = id.Split('-');
                    //int user_id = Convert.ToInt32(userdetail[1].Trim());
                    Distributor distributor = context.Distributors.Where(d => d.d_id == id).FirstOrDefault();
                    DistributorModel distributorModel = new DistributorModel(distributor);
                    return View(distributorModel);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult DistributorAddEditUser(DistributorModel distributorModel)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (distributorModel.d_id == 0)
                    {

                        Distributor distributor = DistributorModel.GetDistributor(distributorModel);
                        distributor.password_lastmodified_dtm = DateTime.Now;
                        context.Distributors.Add(distributor);
                        TempData["Message"] = "Distributor - " + distributorModel.company_name + " Added.";
                    }
                    else
                    {
                        Distributor distributor = DistributorModel.GetDistributor(distributorModel);
                        //check password change
                        distributor.password_lastmodified_dtm = DateTime.Now;
                        context.Entry(distributor).State = EntityState.Modified;
                        TempData["Message"] = "Distributor - " + distributorModel.company_name + " is Updated.";
                    }
                    context.SaveChanges();

                }
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CustomerEditUser(int id)
        {
            if (Session["Username"] != null)
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.c_id == id);
                CustomerModel customerModel = new CustomerModel(customer);
                return View(customerModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult CustomerEditUser(CustomerModel customerModel)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    Customer customer = CustomerModel.GetCustomer(customerModel);
                    Customer_Address customer_Address = Customer_AddressModel.GetCustomer_Address(customerModel.Customer_Address);
                    //check password change

                    context.Entry(customer).State = EntityState.Modified;

                    context.Entry(customer_Address).State = EntityState.Modified;
                    context.SaveChanges();

                    TempData["Message"] = "Customer - " + customer.full_name + " is Updated.";

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
                        Admin admin = context.Admins.FirstOrDefault(a => a.a_id == user_id);
                        context.Admins.Remove(admin);
                        break;
                    case nameof(UserTypeEnum.Customer):
                        Customer customer = context.Customers.FirstOrDefault(c => c.c_id == user_id);
                        context.Customers.Remove(customer);
                        break;
                    case nameof(UserTypeEnum.Distributor):
                        Distributor distributor = context.Distributors.FirstOrDefault(d => d.d_id == user_id);
                        context.Distributors.Remove(distributor);
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

        #region Add Schedule


        public ActionResult DeliveryTimeSlotList()
        {
            if (Session["Username"] != null)
            {
                List<Delivery_Time_SlotModel> delivery_Time_SlotModels = new List<Delivery_Time_SlotModel>();
                context.Delivery_Time_Slot.ToList().ForEach(d =>
                {
                    delivery_Time_SlotModels.Add(new Delivery_Time_SlotModel(d));
                });
                return View(delivery_Time_SlotModels);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        /// <summary>
        /// Schedule Time to Delivery
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEditSchedule(int id)
        {
            if (Session["Username"] != null)
            {
                if (id == 0)
                {
                    return View(new Delivery_Time_SlotModel());
                }
                else
                {
                    Delivery_Time_Slot delivery_Time_Slot = context.Delivery_Time_Slot.FirstOrDefault(d => d.dts_id == id);
                    Delivery_Time_SlotModel delivery_Time_SlotModel = new Delivery_Time_SlotModel(delivery_Time_Slot);
                    return View(delivery_Time_SlotModel);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult AddEditSchedule(Delivery_Time_SlotModel delivery_Time_SlotModel)
        {
            if (Session["Username"] != null)
            {
                if (delivery_Time_SlotModel.dts_id == 0)
                {


                    Delivery_Time_Slot delivery_Time_Slot = Delivery_Time_SlotModel.GetDelivery_Time_Slot(delivery_Time_SlotModel);
                    context.Delivery_Time_Slot.Add(delivery_Time_Slot);
                   
                    TempData["Message"] = "Delivery Time Slot - " + delivery_Time_SlotModel.name + " Added.";
                }
                else
                {
                    Delivery_Time_Slot delivery_Time_Slot = Delivery_Time_SlotModel.GetDelivery_Time_Slot(delivery_Time_SlotModel);
                    context.Entry(delivery_Time_Slot).State = EntityState.Modified;
                    TempData["Message"] = "Delivery Time Slot - " + delivery_Time_Slot.name + " is Updated.";
                }
                context.SaveChanges();
                return RedirectToAction("DeliveryTimeSlotList");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult DeleteDTS(int id)
        {
            if (Session["Username"] != null)
            {
                Delivery_Time_Slot deliveryTimeSlot = context.Delivery_Time_Slot.FirstOrDefault(d => d.dts_id == id);
                context.Delivery_Time_Slot.Remove(deliveryTimeSlot);
                context.SaveChanges();
                TempData["Message"] = "Delivery Time Slot Deleted.";
                return RedirectToAction("DeliveryTimeSlotList");
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
                var order=context.Orders.FirstOrDefault(o => o.o_id==id);
                
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

        #region Login - Logout - Admin
        public ActionResult Login()
        {
            ViewBag.Title = "The Grocery Store - Admin";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
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