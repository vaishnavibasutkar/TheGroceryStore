using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
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
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            //   ViewBag.NoofItemInCart=context.Carts.Where(c=>c.)
            return View();
        }

        public ActionResult ProductByProductCategory(int id = 0)
        {
            Product_Category product_Category = context.Product_Category.FirstOrDefault(pc => pc.pc_id == id);
            //product_Category.Products.ToList().ForEach(p => p.imagepath = Server.MapPath(p.imagepath));
            if (Session["id"] != null)
            {
                int user_id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == user_id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            return View(product_Category);
        }
        public ActionResult ProductDetails(int id = 0)
        {
            Product product = context.Products.FirstOrDefault(p => p.p_id == id);
            //product_Category.Products.ToList().ForEach(p => p.imagepath = Server.MapPath(p.imagepath));
            if (Session["id"] != null)
            {
                int user_id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == user_id).ToList();
            }
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
            var carts = context.Carts.Where(c => c.c_id == c_id).ToList();
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
            if (!context.Cart_Product_Assoc.Where(cpa => cpa.cart_id == cartId).Any())
            {
                Cart _cart = context.Carts.FirstOrDefault(c => c.cart_id == cartId);
                context.Carts.Remove(_cart);
                context.SaveChanges();
            }
            var carts = context.Carts.Where(c => c.c_id == c_id).ToList();
            Session["CartCounter"] = carts.Count;
            Session["Cart"] = carts;
            var cart = carts.FirstOrDefault(c => c.cart_id == cartId);
            var i = cart != null && cart.Cart_Product_Assoc.Any() ? carts.FirstOrDefault(c => c.cart_id == cartId).Cart_Product_Assoc.Count() : 0;
            return Json(new { Success = true, Counter = carts.Count, cartTCount = i }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewCart(int id)
        {
            if (Session["id"] != null)
            {
                int user_id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == user_id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            var c_id = Convert.ToInt32(Session["id"]);
            var cart = context.Carts.FirstOrDefault(c => c.cart_id == id && c.c_id == c_id);
            return View(cart);
        }

        [HttpPost]
        public ActionResult ViewCart()
        {
            //todo
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
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
        public ActionResult ProceedToCheckout(int id)
        {
            if (Session["id"] != null)
            {
                int user_id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == user_id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            CheckoutModel checkoutModel = new CheckoutModel();
            var c_id = Convert.ToInt32(Session["id"]);
            var customer = context.Customers.FirstOrDefault(c => c.c_id == c_id);
            checkoutModel.customer = new CustomerModel(customer);

            Cart cart = new Cart();
            var cpas = new List<Cart_Product_Assoc>();
            Cart_Product_Assoc cpa = new Cart_Product_Assoc();
            cpa.Product = context.Products.FirstOrDefault(p => p.p_id == id);
            cpas.Add(cpa);
            cart.Cart_Product_Assoc = cpas;
            checkoutModel.cart = cart;

            return View(checkoutModel);
        }


        [HttpPost]

        public ActionResult GetDeliveryDate(DateTime SelectedDeliveryDate)
        {
            var DeliveryTimeSlot = context.Delivery_Time_Slot.Where(dts => DbFunctions.TruncateTime(dts.start_datetime) == SelectedDeliveryDate).ToList();
            List<DeliveryTime> DeliveryTime = new List<DeliveryTime>();
            DeliveryTime.Add(new Models.DeliveryTime() { dts_id = 23 });
            DeliveryTime.Add(new Models.DeliveryTime() { dts_id = 2 });
            return Json(new { DeliveryTimeSlots = DeliveryTime }, JsonRequestBehavior.AllowGet);
        }

        private string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            string txnid1 = strHash.ToString().Substring(0, 20);

            return txnid1;
        }

        private string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        //public class RemotePost
        //{
        //    private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


        //    public string Url = "";
        //    public string Method = "post";
        //    public string FormName = "form1";

        //    public void Add(string name, string value)
        //    {
        //        Inputs.Add(name, value);
        //    }

        //    public void Post()
        //    {
        //        System.Web.HttpContext.Current.Response.Clear();

        //        System.Web.HttpContext.Current.Response.Write("<html><head>");

        //        System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
        //        System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
        //        for (int i = 0; i < Inputs.Keys.Count; i++)
        //        {
        //            System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
        //        }
        //        System.Web.HttpContext.Current.Response.Write("</form>");
        //        System.Web.HttpContext.Current.Response.Write("</body></html>");

        //        System.Web.HttpContext.Current.Response.End();
        //    }
        [HttpPost]
        public ActionResult ProceedToCheckout(FormCollection form)
        {
            //try
            //{
            //    string firstName = "vaishnavi";
            //    string amount = "10";
            //    string productInfo = "Hello product";
            //    string email = "vaishnavibasutkar2@gmail.com";
            //    string phone = "9594342419";
            //    string surl = "https://localhost:44350/Customer/home";  //Change the success url here depending upon the port number of your local system.
            //    string furl = "https://localhost:44350/Customer/home";  //Change the failure url here depending upon the port number of your local system.




            //    RemotePost myremotepost = new RemotePost();
            //    //Add your MarchantID;  
            //    string key = "add your MarchantID";
            //    //Add your SaltID;  
            //    string salt = "add your SaltID";

            //    //posting all the parameters required for integration.  

            //    myremotepost.Url = "https://sandboxsecure.payu.in/_payment";
            //    myremotepost.Add("key", "4ptctt6n");
            //    string txnid = Generatetxnid();
            //    myremotepost.Add("txnid", txnid);
            //    myremotepost.Add("amount", amount);
            //    myremotepost.Add("productinfo", productInfo);
            //    myremotepost.Add("firstname", firstName);
            //    myremotepost.Add("phone", phone);
            //    myremotepost.Add("email", email);
            //    myremotepost.Add("surl", "http://localhost:55447/Return/Return");//Change the success url here depending upon the port number of your local system.  
            //    myremotepost.Add("furl", "http://localhost:55447/Return/Return");//Change the failure url here depending upon the port number of your local system.  
            //    myremotepost.Add("service_provider", "payu_paisa");
            //    string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
            //    string hash = Generatehash512(hashString);
            //    myremotepost.Add("hash", hash);

            //    myremotepost.Post();
            //}
            //catch (Exception exp)
            //{
            //    throw;
            //}
            return View();
        }

        public ActionResult ProceedToCheckoutFromCart(List<Cart_Product_Assoc> ids)
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            CheckoutModel checkoutModel = new CheckoutModel();
            var c_id = Convert.ToInt32(Session["id"]);
            var customer = context.Customers.FirstOrDefault(c => c.c_id == c_id);
            checkoutModel.customer = new CustomerModel(customer);

            //Cart cart = new Cart();
            //var cpas = new List<Cart_Product_Assoc>();
            //foreach (var id in ids)
            //{
            //    Cart_Product_Assoc cpa = new Cart_Product_Assoc();
            //    cpa.Product = context.Products.FirstOrDefault(p => p.p_id == id);
            //    cpas.Add(cpa);
            //}
            //cart.Cart_Product_Assoc = cpas;
            //checkoutModel.cart = cart;

            return View(checkoutModel);
        }

        #region Login - Logout - Customer
        public ActionResult LoginCustomer()
        {
            var test = Request.Cookies["mycookie"].Value;
            ViewBag.Title = "The Grocery Store";
            return View();
        }

        [HttpPost]
        public ActionResult LoginCustomer(string PhoneNumber)
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
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
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
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
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Session["Cart"] = context.Carts.Where(c => c.c_id == id).ToList();
            }
            ViewBag.Category = context.Product_Category.ToList();
            return View();
        }
    }
}