using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShopDemirci.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameShopDemirci.Controllers
{
    public class CartController : Controller
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
            var games = Cart.Games;
            decimal totalPrice = 0;

            foreach (var game in games)
            {
                totalPrice = totalPrice + game.gamePrice;
            }

            CartViewModel viewModel = new CartViewModel(games,totalPrice);
            return View(viewModel);
        }
    }
}