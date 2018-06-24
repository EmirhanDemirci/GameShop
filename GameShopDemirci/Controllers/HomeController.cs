using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShopDemirci.Models;
using Microsoft.Extensions.Configuration;

namespace GameShopDemirci.Controllers
{
    public class HomeController : Controller
    {
        string _connectionString;

        public HomeController(IConfiguration config)
        {
            _connectionString = config.GetSection("Connection")["MSSQL"];
        }
        public IActionResult Index()
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public IActionResult About()
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
     

        public IActionResult Contact()
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Contact(Users users)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            var query = "INSERT INTO Users(FirstName, LastName, Email) VALUES('{0}', '{1}', '{2}')";
            var queryFull = string.Format(query, users.FirstName, users.LastName, users.Email);
            var sc = new SqlConnection(_connectionString);
            sc.Open();
            using (SqlCommand cmd = new SqlCommand(queryFull, sc))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    sc.Close();
                }
                catch
                {

                    sc.Close();
                }
            }

            // hoi
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
