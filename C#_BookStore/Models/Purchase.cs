using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__BookStore.Models
{
    public class Purchase
    {
        public int purchase_id { get; set; }
        public string user_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public float total_price { get; set; }
        public DateTime purchase_date { get; set; }
    }
}