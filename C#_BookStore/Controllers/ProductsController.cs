using C__BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C__BookStore.Controllers
{
    public class ProductsController : Controller
    {

        DbAccess db = new DbAccess();
        Random rand = new Random();

        public ActionResult addProducts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addProducts(Products prod)
        {

            String fname = prod.img_file.FileName;
            string ext = Path.GetExtension(fname);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" ,".git", ".bnp"};
            if (!allowedExtensions.Contains(ext.ToLower()))
            {
                ViewBag.message = "Invalid file format. Please upload a .jpg, .jpeg, .png, .git, or .bnp file.";
                return View();
            }

            string u_id = Session["uid"].ToString();

            int num = rand.Next(1000, 9999);
            String serverpath = $"{u_id}_{num}_{fname}";
            String path = Server.MapPath("~/Images/" + serverpath);

            prod.img_file.SaveAs(path);
            prod.img_path = "/Images/" + serverpath;

            db.OpenConnection();
            String query = $"insert into Products values ('{u_id}', '{prod.product_name}', {prod.quantity}, {prod.price}, '{prod.img_path}')";
            db.InsertUpdateDelete(query);
            db.CloseConnection();

            Console.WriteLine(prod.img_path);

            return View(prod);
        }

        public ActionResult showProducts()
        {
            List<Products> products = new List<Products>();
            String query = "select product_id, product_name, quantity, price, img_path from Products";
            db.OpenConnection();
            SqlDataReader sdr = db.GetData(query);

            while (sdr.Read())
            {
                products.Add(new Products()
                {
                    product_id = int.Parse(sdr["product_id"].ToString()),
                    product_name = sdr["product_name"].ToString(),
                    price = float.Parse(sdr["price"].ToString()),
                    quantity = int.Parse(sdr["quantity"].ToString()),
                    img_path = sdr["img_path"].ToString()
                });
            }
            db.CloseConnection();
            return View(products);
        }
    }
}