using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{
    public class DeliveryTime
    {
        public DeliveryTime()
        {

        }
        public DeliveryTime(int id, string dtm)
        {
            dts_id = id;
            dts_dtm = dtm;
        }

        public int dts_id { get; set; }
        public String dts_dtm { get; set; }
    }
}