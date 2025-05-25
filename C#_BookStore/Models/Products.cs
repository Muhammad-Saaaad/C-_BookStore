using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__BookStore.Models
{
    public class Products
    {
        public string user_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }

        public string img_path { get; set; }
        public HttpPostedFileBase img_file { get; set; }
    }
}