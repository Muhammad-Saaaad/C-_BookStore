using C__BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace C__BookStore.Controllers
{
    public class AccountsController : Controller
    {
        DbAccess db = new DbAccess();

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignUp(Accounts acc)
        {
            String match_email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(acc.email, match_email))
            {
                 acc.user_id = acc.email.Split('@')[0];

                db.OpenConnection();
                String query = $"insert into Accounts values ('{acc.user_id}', '{acc.email}','{acc.password}', '{acc.user_type}') ";
                db.InsertUpdateDelete(query);
                db.CloseConnection();

                ViewBag.user_id = acc.user_id;

                ViewBag.message = "Account created successfully!";
                return View();
            }
            else
            {
                ViewBag.message = "Invalid email format.";
                return View();
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Accounts acc)
        {
            Session.Clear();

            // Clear cookies if they exist
            if (Request.Cookies["user_id"] != null)
            {
                HttpCookie cookies_obj = new HttpCookie("user_id");
                cookies_obj.Expires = DateTime.Now.AddDays(-1); // Set cookie expiration to a past date to delete it
                Response.Cookies.Add(cookies_obj);
            }

            String query = $"select * from Accounts where user_id='{acc.user_id}' and password='{acc.password}'";
            db.OpenConnection();
            SqlDataReader sdr = db.GetData(query);

            if (sdr.Read())
            {
                Session["uid"] = sdr["user_id"].ToString();

                HttpCookie cookies_obj = new HttpCookie("user_id");
                cookies_obj["id"] = sdr["user_id"].ToString();
                cookies_obj.Expires = DateTime.Now.AddMinutes(30); // Set cookie expiration to 30 minutes
                Response.Cookies.Add(cookies_obj);

                if (sdr["user_type"].ToString() == "buyer")
                {
                    db.CloseConnection();
                    return RedirectToAction("showProducts", "Products");
                }
                else // if seller
                {
                    db.CloseConnection();
                    return RedirectToAction("addProducts", "Products");
                }
            }
            else
            {
                ViewBag.message = "Invalid user id or password.";
                db.CloseConnection();
                return View();
            }
        }
    }
}