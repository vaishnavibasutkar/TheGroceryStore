using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GroceryStoreMain.Controllers
{
    public class CustomerController : Controller
    {
        GroceryStoreDBEntities context;
        private List<Cart> ListofCart;
        public CustomerController()
        {
            context = new GroceryStoreDBEntities();
            ListofCart = new List<Cart>();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            var customer = context.Customers.FirstOrDefault(c => c.c_id == 2);
            //Session["Username"] = customer.email_id;
            //Session["Name"] = customer.full_name;
            //Session["id"] = customer.c_id;
            //Session["Cart"] = context.Carts.Where(c => c.c_id == customer.c_id).ToList();

            ViewBag.Category = context.Product_Category.ToList();
            //   ViewBag.NoofItemInCart=context.Carts.Where(c=>c.)
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

        //public ActionResult AddToCart(int id)
        //{
        //    if (Session["Username"] != null)
        //    {
        //        int p_id = Convert.ToInt32(Request.QueryString["pid"]);
        //        Cart cart = context.Carts.FirstOrDefault(c => c.cart_id == id);

        //        Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
        //        cart_Product_Assoc.cart_id = cart.cart_id;
        //        cart_Product_Assoc.p_id = p_id;
        //        context.Cart_Product_Assoc.Add(cart_Product_Assoc);
        //        context.SaveChanges();
        //        return View();
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        //[HttpPost]
        //public ActionResult AddToCart()
        //{
        //    return Json(new
        //    {
        //        Success = true,
        //    });
        //}
        //[HttpPost]
        //public ActionResult AddToCart(int cartId)
        //{
        //    return Json(new
        //    {
        //        Success = true,
        //    });
        //}
        //[HttpPost]
        //public ActionResult AddToCart(string cartId)
        //{

        //    int p_id = Convert.ToInt32(Request.QueryString["pid"]);

        //    Cart cart = context.Carts.FirstOrDefault(c => c.cart_id.ToString() == cartId && c.c_id == Convert.ToInt32(Session["id"]));
        //    Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
        //    cart_Product_Assoc.cart_id = cart.cart_id;
        //    cart_Product_Assoc.p_id = p_id;


        //    context.Cart_Product_Assoc.Add(cart_Product_Assoc);
        //    context.SaveChanges();
        //    var carts = context.Cart_Product_Assoc.Where(c => c.cart_id == cart.cart_id).ToList();
        //    Session["CartCounter"] = carts.Count;
        //    Session["Cart"] = carts;
        //    return Json(new { Success = true, Counter = carts.Count }, JsonRequestBehavior.AllowGet);


        //}

        [HttpPost]
        public ActionResult AddNewCart()
        {
            if (Session["Username"] != null)
            {
                var c_id = Convert.ToInt32(Session["id"]);
                Cart cart = new Cart();
                cart.name = "Name";
                cart.comment = "Comment";
                cart.c_id = c_id;
                context.Carts.Add(cart);

                context.SaveChanges();

                Session["Cart"] = context.Carts.Where(c => c.c_id == c_id).ToList();
                ViewBag.Category = context.Product_Category.ToList();
                return Json(null);

            }
            return Json(null);
        }
        [HttpPost]
        public ActionResult AddToCartNew(string[] IDs)
        {
            int cartId = Convert.ToInt32(IDs[0]);
            int p_id = Convert.ToInt32(IDs[1]);
            int c_id = Convert.ToInt32(Session["id"]);
            Cart cart = context.Carts.FirstOrDefault(c => c.cart_id == cartId && c.c_id == c_id);
            Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
            cart_Product_Assoc.cart_id = cart.cart_id;
            cart_Product_Assoc.p_id = p_id;


            context.Cart_Product_Assoc.Add(cart_Product_Assoc);
            context.SaveChanges();
            var carts = context.Carts.Where(c=>c.c_id==c_id).ToList();
            Session["CartCounter"] = carts.Count;
            Session["Cart"] = carts;
            var i = carts.FirstOrDefault(c => c.cart_id == cartId).Cart_Product_Assoc.Count();
            return Json(new { Success = true, Counter = carts.Count, cartTCount = i }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RemoveFromCart(string[] IDs)
        {
            int cartId = Convert.ToInt32(IDs[0]);
            int p_id = Convert.ToInt32(IDs[1]);
            int c_id = Convert.ToInt32(Session["id"]);
            var todelete = context.Cart_Product_Assoc.Where(c => c.cart_id == cartId && c.p_id == p_id);
            context.Cart_Product_Assoc.RemoveRange(todelete);
            context.SaveChanges();
            var carts = context.Carts.Where(c => c.c_id == c_id).ToList();
            Session["CartCounter"] = carts.Count;
            Session["Cart"] = carts;
            var i = carts.FirstOrDefault(c => c.cart_id == cartId).Cart_Product_Assoc.Count();
            return Json(new { Success = true, Counter = carts.Count, cartTCount = i }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewCart(int id)
        {
            ViewBag.Category = context.Product_Category.ToList();
            var c_id = Convert.ToInt32(Session["id"]);
            var cart = context.Carts.FirstOrDefault(c => c.cart_id == id && c.c_id == c_id);
            return View(cart);
        }

        [HttpPost]
        public ActionResult ViewCart()
        {
            //todo
            ViewBag.Category = context.Product_Category.ToList();
            if (Session["Username"] != null)
            {
                Cart cart = new Cart();
                cart.name = "Name";
                cart.comment = "Comment";
                context.Carts.Add(cart);

                context.SaveChanges();
                return Json(null);

            }
            return Json(null);
        }


        #region Login - Logout - Customer
        public ActionResult LoginCustomer()
        {
            ViewBag.Title = "The Grocery Store";
            return View();
        }

        [HttpPost]
        public ActionResult LoginCustomer(string PhoneNumber)
        {

            ViewBag.Category = context.Product_Category.ToList();
            //Generate OTP
            //Save OTP

            //Send OTP - email and phone number
            //show enter OTP Screen


            decimal phoneno = Convert.ToDecimal(PhoneNumber);
            Random rnd = new Random();
            string randomNumber = (rnd.Next(100000, 999999)).ToString();
            var customer = context.Customers.FirstOrDefault(c => c.mobile_no == phoneno);
            if (customer == null)
            {
                context.Customers.Add(new Customer()
                {
                    // mobile_no = Convert.ToDecimal(PhoneNumber),
                    full_name = PhoneNumber,
                    email_id = PhoneNumber

                });
                context.SaveChanges();
                customer = context.Customers.FirstOrDefault(c => c.mobile_no == phoneno);
            }

            OTP_History oTP_History = new OTP_History();
            oTP_History.c_id = customer.c_id;

            oTP_History.otp = Convert.ToDecimal(randomNumber);
            oTP_History.eff_startdtm = DateTime.Now;
            oTP_History.eff_enddtm = DateTime.Now.AddMinutes(5);
            context.OTP_History.Add(oTP_History);

            context.SaveChanges();
            try
            {
                //send sms

            }
            catch (Exception ex) { }
            //tofdo verify otp is correct

            return Json(new { success = true, phoneno = PhoneNumber.Substring(PhoneNumber.Length - 4) });
        }


        [HttpPost]
        public ActionResult LoginCustomerEmail(string EmailId)
        {

            ViewBag.Category = context.Product_Category.ToList();
            //Generate OTP
            //Save OTP

            //Send OTP - email and phone number
            //show enter OTP Screen


            Random rnd = new Random();
            string randomNumber = (rnd.Next(100000, 999999)).ToString();
            var customer = context.Customers.FirstOrDefault(c => c.email_id == EmailId);
            if (customer == null)
            {
                context.Customers.Add(new Customer()
                {
                    // mobile_no = Convert.ToDecimal(PhoneNumber),
                    full_name = EmailId,
                    email_id = EmailId

                });
                context.SaveChanges();
                customer = context.Customers.FirstOrDefault(c => c.email_id == EmailId);
            }

            OTP_History oTP_History = new OTP_History();
            oTP_History.c_id = customer.c_id;

            oTP_History.otp = Convert.ToDecimal(randomNumber);
            oTP_History.eff_startdtm = DateTime.Now;
            oTP_History.eff_enddtm = DateTime.Now.AddMinutes(5);
            context.OTP_History.Add(oTP_History);

            context.SaveChanges();
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("Vaishnavibasutkar2@gmail.com");
                message.To.Add(new MailAddress(EmailId));
                message.Subject = "Test";
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = "Hi, Your OTP is " + randomNumber;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("vaishnavibasutkar2@gmail.com", "Swamini37");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex) { }
            //tofdo verify otp is correct

            return Json(new { success = true, emailId = EmailId.Substring(EmailId.Length - 9) });
        }
        [HttpPost]
        public ActionResult ValidateOTP(String otptext)
        {
            var otp = Convert.ToDecimal(otptext);
            var time = DateTime.Now;
            var result = context.OTP_History.Where(o => o.otp == otp && o.eff_startdtm <= time && o.eff_enddtm >= time);

            if (result.Any())
            {
                var customer = result.FirstOrDefault().Customer;
                Session["Username"] = customer.email_id;
                Session["Name"] = customer.full_name;
                Session["id"] = customer.c_id;
                Session["Cart"] = context.Carts.Where(c => c.c_id == customer.c_id).ToList();
                return Json(new { success = true, Name = Session["Name"] });
            }
            else
            {
                return Json(new { success = false, message = "Invalid OTP. Try Again." });
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Home");
        }


        #endregion


        public ActionResult Blank()
        {
            ViewBag.Category = context.Product_Category.ToList();
            return View();
        }
    }
}