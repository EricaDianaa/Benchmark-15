using Benchmark_15.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Benchmark_15.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Exclude="Ruolo")] Utenti u)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select * from Utenti where Username=@Username and Password=@password", conn);
            SqlDataReader sqlreader;

            conn.Open();

            cmd1.Parameters.AddWithValue("Username", u.Username);
            cmd1.Parameters.AddWithValue("password", u.Password);

            sqlreader = cmd1.ExecuteReader();
            if (sqlreader.HasRows)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
            }
            else
            {
                ViewBag.ErrorMessage = "Autentificazione non riuscita";
                return RedirectToAction("Login", "Home");
            }
            conn.Close();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}