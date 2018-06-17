using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShopDemirci.Models;

namespace GameShopDemirci.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
