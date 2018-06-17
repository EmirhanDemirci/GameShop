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
    public class GameController : Controller
    {
        string _connectionString;
        private Cart _cart;

        public GameController(IConfiguration config)
        {
            _connectionString = config.GetSection("Connection")["MSSQL"];
            _cart = new Cart(_connectionString);
        }

      
        public IActionResult PcView(Games objGames)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var games = GetGames(id:0);
            return View(games);
            
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int platformId)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }

            _cart.AddGame(id);

            switch (platformId)
            {
                case 0:
                    return RedirectToAction("PcView");         
                case 1:
                    return RedirectToAction("Ps4View");           
                case 2:
                    return RedirectToAction("XboxView");        
                case 3:
                    return RedirectToAction("NintendoView");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Ps4View(Games objGames)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var games = GetGames(id: 1);
            return View(games);
        }
        public IActionResult XboxView(Games objGames)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var games = GetGames(id: 2);
            return View(games);
        }
        public IActionResult NintendoView(Games objGames)
        {
            if (Accounts.Account != null)
            {
                ViewData["Username"] = Accounts.Account.UserName;
            }
            if (Accounts.Account == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var games = GetGames(id: 3);
            return View(games);
        }
        public List<Games> GetGames( int id)
        {
            List<Games> games = new List<Games>();
            var game = new Games(); 
            var query = "SELECT * FROM games WHERE platformId =" + id;
            var sc = new SqlConnection(_connectionString);

            sc.Open();

            using (SqlCommand cmd = new SqlCommand(query, sc))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    game = new Games()
                    {
                        gameId = reader.GetInt32(0),
                        gameName = reader.GetString(1),
                        gameDescription = reader.GetString(2),
                        gameImg = reader.GetString(3),
                        gamePrice = reader.GetDecimal(4)
                    };
                    games.Add(game);
                }

            }

            return games;
        } 

    }
}