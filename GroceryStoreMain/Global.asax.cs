using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GroceryStoreMain
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static System.Timers.Timer timer;
      
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            timer = new System.Timers.Timer(300000);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OtpTimerUP);
        }
        static void OtpTimerUP(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Do Your Stuff
            GroceryStoreDBEntities context = new GroceryStoreDBEntities();
            context.OTP_History.Where(o => o.eff_enddtm < DateTime.Now)
                               .ToList()
                               .ForEach(o => context.OTP_History.Remove(o));
            context.SaveChanges();

        }
    }
}
