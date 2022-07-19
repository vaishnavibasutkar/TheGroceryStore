using GroceryStoreMain.Models;
using GroceryStoreMain.Payment;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
            List<Product_Category> pcs = context.Product_Category.ToList();
            pcs.ForEach(p =>
            {
                if (p.imagepath != null)
                {
                    p.imagepath = Path.Combine("~\\Images\\ProductCategories", p.imagepath+".jpg");
                }

            });
            ViewBag.Category = pcs;
            ViewBag.NewArrivalProduct = context.Products.OrderBy(p => p.p_id).Take(8).ToList();
            //   ViewBag.NoofItemInCart=context.Carts.Where(c=>c.)
            return View();
        }


        public ActionResult CustomerHome(string search)
        {
            ViewBag.Category = context.Product_Category.ToList();
            if (search == null || search == "")
            {
                var products = context.Products.ToList();
                return RedirectToAction("Home");
            }
            else
            {

                context.Configuration.ProxyCreationEnabled = false;
                var terms = search.Split(' ');

                var results = context.Products
                    .Where(p => terms.Any(term => p.name.Contains(term)))
                    .Select(p => new ProductModel
                    {
                        p_id = p.p_id,
                        name = p.name,
                        price = p.price,
                        imagepath = p.imagepath.ToString()

                    }).ToList();
                var recipes = context.Recipes.ToList();
                //return Json(new { success = true, products = results, url = "https://localhost:44350/Customer/CustomerHome" }, JsonRequestBehavior.AllowGet);
                return View(results);

            }
        }

        public ActionResult MyOrders()
        {
            var c_id = Convert.ToInt32(Session["id"]);
            //todo: thinknig of adding order number. but for now i will use order id
            var orders = context.Orders.Where(o => o.c_id == c_id).ToList();
            ViewBag.Category = context.Product_Category.ToList();
            return View(orders);

        }

        public ActionResult MyOrderDetails(int id)
        {
            ViewBag.Category = context.Product_Category.ToList();
            var order = context.Orders.FirstOrDefault(o => o.o_id == id);
            order = order.GetOrder(order);
            return View(order);
        }

        public ActionResult SubmitFeedback(string feedback)
        {
            Feedback f = new Feedback();
            f.c_id = Convert.ToInt32(Session["id"]);
            Feedback feedback1 = JsonSerializer.Deserialize<Feedback>(feedback);
            feedback1.c_id = Convert.ToInt32(Session["id"]);
            context.Feedbacks.Add(feedback1);
            context.SaveChanges();
            return Json(new { success = true, url = "https://localhost:44350/Customer/CustomerHome" });
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
            if (Session["id"] == null)
            {
                
                return RedirectToAction("Home");
            }
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
            cart.name = "Name";
            cart.comment = "Comment";
            cart.c_id = c_id;
            context.Carts.Add(cart);

            context.SaveChanges();
            var cpas = new List<Cart_Product_Assoc>();
            Cart_Product_Assoc cpa = new Cart_Product_Assoc();
            cpa.Product = context.Products.FirstOrDefault(p => p.p_id == id);
            cpa.p_id = cpa.Product.p_id;
            cpa.cart_id = cart.cart_id;
            context.Cart_Product_Assoc.Add(cpa);

            context.SaveChanges();
            cpas.Add(cpa);
            cart.Cart_Product_Assoc = cpas;
            checkoutModel.cart = cart;

            return View(checkoutModel);
        }



        [HttpPost]

        public ActionResult GetDeliveryDate(DateTime SelectedDeliveryDate)
        {
            context.Configuration.ProxyCreationEnabled = false;
            var DeliveryTimeSlot = context.Delivery_Time_Slot.Where(dts => DbFunctions.TruncateTime(dts.start_datetime) == SelectedDeliveryDate).ToList();
            List<DeliveryTime> DeliveryTime = new List<DeliveryTime>();

            foreach (var dts in DeliveryTimeSlot)
            {
                var name = dts.start_datetime.ToShortTimeString() + " - " + dts.start_datetime.AddMinutes(dts.duration).ToShortTimeString();
                DeliveryTime.Add(new Models.DeliveryTime() { dts_id = dts.dts_id, dts_dtm = name });
            }
            context.Configuration.ProxyCreationEnabled = true;
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
        //[HttpPost]
        //public ActionResult ProceedToCheckout(FormCollection form)
        //{
        //    //try
        //    //{
        //    //    string firstName = "vaishnavi";
        //    //    string amount = "10";
        //    //    string productInfo = "Hello product";
        //    //    string email = "vaishnavibasutkar2@gmail.com";
        //    //    string phone = "9594342419";
        //    //    string surl = "https://localhost:44350/Customer/home";  //Change the success url here depending upon the port number of your local system.
        //    //    string furl = "https://localhost:44350/Customer/home";  //Change the failure url here depending upon the port number of your local system.




        //    //    RemotePost myremotepost = new RemotePost();
        //    //    //Add your MarchantID;  
        //    //    string key = "add your MarchantID";
        //    //    //Add your SaltID;  
        //    //    string salt = "add your SaltID";

        //    //    //posting all the parameters required for integration.  

        //    //    myremotepost.Url = "https://sandboxsecure.payu.in/_payment";
        //    //    myremotepost.Add("key", "4ptctt6n");
        //    //    string txnid = Generatetxnid();
        //    //    myremotepost.Add("txnid", txnid);
        //    //    myremotepost.Add("amount", amount);
        //    //    myremotepost.Add("productinfo", productInfo);
        //    //    myremotepost.Add("firstname", firstName);
        //    //    myremotepost.Add("phone", phone);
        //    //    myremotepost.Add("email", email);
        //    //    myremotepost.Add("surl", "http://localhost:55447/Return/Return");//Change the success url here depending upon the port number of your local system.  
        //    //    myremotepost.Add("furl", "http://localhost:55447/Return/Return");//Change the failure url here depending upon the port number of your local system.  
        //    //    myremotepost.Add("service_provider", "payu_paisa");
        //    //    string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
        //    //    string hash = Generatehash512(hashString);
        //    //    myremotepost.Add("hash", hash);

        //    //    myremotepost.Post();
        //    //}
        //    //catch (Exception exp)
        //    //{
        //    //    throw;
        //    //}
        //    return View();
        //}

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

        public CheckoutModel CheckoutModel { get; set; }

        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, Customer customer, Cart cart, Models.Order order)
        {

            //Models.Order order = new Models.Order();

            //context.Orders.Add(order);
            //context.SaveChanges();

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            foreach (var item in cart.Cart_Product_Assoc)//cpas
            {
                //Adding Item Details like name, currency, price etc  
                itemList.items.Add(new Item()
                {
                    name = item.Product.name,
                    currency = "USD",
                    price = item.Product.price.ToString(),
                    quantity = "1",
                    sku = item.Product.Product_Category.name
                });
            }

            Address address = new Address()
            {
                line1 = customer.Customer_Address.address1 + customer.Customer_Address.address2,
                line2 = customer.Customer_Address.address3,
                city = customer.Customer_Address.city,
                country_code = "IN",
                state = customer.Customer_Address.state,
                postal_code = customer.Customer_Address.postal_code
            };
            PayerInfo payerInfo = new PayerInfo()
            {
                first_name = customer.full_name,
                billing_address = address,
                email = customer.email_id,
                country_code = "IN"
            };

            var payer = new Payer()
            {
                payment_method = "paypal",
                payer_info = payerInfo
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = order.tax.ToString(),//total
                shipping = order.delivery_charge.ToString(),//shipping
                subtotal = order.total_amount.ToString()//
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = Convert.ToInt32(order.total_amount).ToString(),
                //total = this.CheckoutModel.total.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
        public ActionResult Success()
        {
            ViewBag.Category = context.Product_Category.ToList();
            return View();
        }

        [HttpPost]
        public object ProceedToCheckout(string DeliveryTimeSlot, string SelectedPaymentMode, string CustomerDetails, string IsShipToDiffAddress, string CustomerShippingAdd, int cart_id)
        {
            var c_id = Convert.ToInt32(Session["id"]);
            Customer customer = JsonSerializer.Deserialize<Customer>(CustomerDetails);
            Delivery_Time_Slot dts = new Delivery_Time_Slot();
            //get customer details from db
            Customer customer1 = context.Customers.FirstOrDefault(c => c.c_id == customer.c_id);
            Customer_Address customer_Address = customer1.Customer_Address;
            if (customer1 != null)
            {
                customer1.full_name = customer.full_name;
                customer1.email_id = customer.email_id;
                customer1.mobile_no = customer.mobile_no;
                if (customer.alternate_mobile_no != null)
                {
                    customer1.alternate_mobile_no = customer.alternate_mobile_no;
                }

                if (customer_Address != null)
                {
                    customer_Address.address1 = customer.Customer_Address.address1;
                    customer_Address.address2 = customer.Customer_Address.address2;
                    customer_Address.address3 = customer.Customer_Address.address3;
                    customer_Address.city = customer.Customer_Address.city;
                    customer_Address.state = customer.Customer_Address.state;
                    customer_Address.country = customer.Customer_Address.country;
                    customer_Address.postal_code = customer.Customer_Address.postal_code;

                    context.Customer_Address.Attach(customer_Address);
                }
                else
                {
                    Customer_Address address = customer.Customer_Address;
                    context.Customer_Address.Add(address);
                    context.SaveChanges();
                    customer1.ca_id = address.ca_id;

                }
                context.Customers.Attach(customer1);

            }

            Customer_Address ca = new Customer_Address();
            if (IsShipToDiffAddress != null)
            {
                ca = JsonSerializer.Deserialize<Customer_Address>(CustomerShippingAdd);
                context.Customer_Address.Add(ca);
                context.SaveChanges();
            }

            Cart cart = context.Carts.FirstOrDefault(c => c.cart_id == cart_id);
            decimal tot = 0;
            cart.Cart_Product_Assoc.ToList().ForEach(cpa => { tot += cpa.Product.price; });

            Models.Order order = new Models.Order();
            order.c_id = c_id;
            order.dts_id = Convert.ToInt32(DeliveryTimeSlot);
            order.order_date = System.DateTime.Now;
            order.os_id = 1002;//Code for NEW in Order
            order.ca_id = ca.ca_id > 0 ? ca.ca_id : customer1.ca_id;
            order.paymentmode = SelectedPaymentMode;
            order.total_amount = tot;
            order.delivery_charge = 0;
            order.tax = 0;
            context.Orders.Add(order);
            context.SaveChanges();


            var cart_Product_Assocs = cart.Cart_Product_Assoc.GroupBy(c => c.p_id)
                            .Select(t => new
                            {
                                pid = t.Key,
                                did = t.Select(f => f.Product.d_id).FirstOrDefault(),
                                Count = t.Count(),
                                price = t.Select(f => f.Product.price).FirstOrDefault()
                            }).ToList();

            foreach (var cpa in cart_Product_Assocs)
            {
                Order_Details od = new Order_Details();
                od.p_id = cpa.pid;
                od.d_id = cpa.did;//check d_id
                od.o_id = order.o_id;
                od.quantity = cpa.Count;
                od.total_amt = cpa.Count * cpa.price;
                context.Order_Details.Add(od);
            }
            context.SaveChanges();

            if (SelectedPaymentMode =="PAYPAL")
            {
                #region place Payment
                //getting the apiContext  
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                try
                {
                    //A resource representing a Payer that funds a payment Payment Method as paypal  
                    //Payer Id will be returned when payment proceeds or click to pay  
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {
                        //this section will be executed first because PayerID doesn't exist  
                        //it is returned by the create function call of the payment class  
                        // Creating a payment  
                        // baseURL is the url on which paypal sendsback the data.  
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/Success?";
                        //here we are generating guid for storing the paymentID received in session  
                        //which will be used in the payment execution  
                        var guid = Convert.ToString((new Random()).Next(100000));
                        //CreatePayment function gives us the payment approval url  
                        //on which payer is redirected for paypal account payment  
                        var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, customer1, cart, order);
                        //get links returned from paypal in response to Create function call  
                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                //saving the payapalredirect URL to which user will be redirected for payment  
                                paypalRedirectUrl = lnk.href;
                            }
                        }
                        // saving the paymentID in the key guid  
                        Session.Add(guid, createdPayment.id);
                        return Json(new { success = true, url = paypalRedirectUrl });
                    }
                    else
                    {
                        // This function exectues after receving all parameters for the payment  
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                        //If executed payment failed then we will show payment failure message to user  
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return View("FailureView");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("FailureView");
                }
                //on successful payment, show success page to user. 
                #endregion
            }

            //todo: delete cart ad cpa

            return Json(new { success = true, url = "https://localhost:44350/Payment/Success" });
        }



        #region 
        public ActionResult MyProfile()
        {
            int id = Convert.ToInt32(Session["id"]);
            ViewBag.Category = context.Product_Category.ToList();
            var customer = context.Customers.FirstOrDefault(c => c.c_id == id);
            return View(customer);
        }
        #endregion

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
            var result = context.OTP_History.FirstOrDefault(o => o.otp == otp && o.eff_startdtm <= time && o.eff_enddtm >= time);

            if (result != null)
            {
                var customer = result.Customer;
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