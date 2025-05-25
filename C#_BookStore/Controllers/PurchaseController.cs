using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using C__BookStore.Models;

namespace C__BookStore.Controllers
{
    public class PurchaseController : Controller
    {
        DbAccess db = new DbAccess();
        public ActionResult buyProducts(String uid, String pid, String p_name, String price, String quantity)
        {
            var model = new Purchase
            {
                user_id = uid, // however you get the user ID
                product_id = int.Parse(pid),
                product_name = p_name,
                total_price = float.Parse(price),
                quantity = Convert.ToInt32(quantity)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(Purchase purchase)
        {
            String query = "select quantity from Products where product_id = " + purchase.product_id;

            db.OpenConnection();
            SqlDataReader sdr = db.GetData(query);
            int quantity = 0;
            if (sdr.Read())
            {
                quantity = int.Parse(sdr["quantity"].ToString());
                sdr.Close();
                db.CloseConnection();
            }

            if (quantity >= purchase.quantity)
            {
                purchase.total_price = purchase.total_price * purchase.quantity;
                var date = DateTime.Now;
                purchase.purchase_date = date.Date;

                query = $"insert into purchase values ('{purchase.user_id}', {purchase.product_id}, '{purchase.product_name}'," +
                    $"{purchase.quantity}, {purchase.total_price}, '{purchase.purchase_date}')";

                db.OpenConnection();
                db.InsertUpdateDelete(query);

                query = $"update Products set quantity = {quantity - purchase.quantity} where product_id = {purchase.product_id} ";
                db.InsertUpdateDelete(query);
                db.CloseConnection();

                ViewBag.purchase = "Purchase Sucessfull";
            }
            else
            {
                ViewBag.purchase = "Purchase Un-Sucessfull as quantity is high";
            }
            return RedirectToAction("showProducts", "Products");
        }
        
    }
}