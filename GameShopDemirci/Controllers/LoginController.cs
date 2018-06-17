using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GameShopDemirci.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GameShopDemirci.Controllers
{
    public class LoginController : Controller
    {
        string _connectionString;

        public LoginController(IConfiguration config)
        {
            _connectionString = config.GetSection("Connection")["MSSQL"];
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Accounts accounts)
        {

            var query = "INSERT INTO Accounts(UserName, Password) VALUES('{0}', '{1}')";
            var queryFull = string.Format(query, accounts.UserName, accounts.Password);
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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Accounts objUser)
        {
            var account = LoginUser(objUser);
            if (account != null)
            {

                Accounts.Account = account;
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        private Accounts LoginUser(Accounts accounts)
        {
            Accounts account = null;
            var query = "SELECT * FROM Accounts WHERE UserName = '{0}' AND Password = '{1}'";
            var queryFull = string.Format(query, accounts.UserName, accounts.Password);
            var sc = new SqlConnection(_connectionString);
            sc.Open();
            using (SqlCommand cmd = new SqlCommand(queryFull, sc))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account = new Accounts()
                    {
                        AccountID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2)
                    };
                }
            }
            return account;
        }

        public IActionResult LogOut()
        {
            if (Accounts.Account != null)
            {
                Accounts.Account = null;
                Cart.Games.Clear();
            }
           
            return RedirectToAction("Login", "Login");
        }
        
    }
}