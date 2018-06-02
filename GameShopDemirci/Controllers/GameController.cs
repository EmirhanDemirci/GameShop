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

        public GameController(IConfiguration config)
        {
            _connectionString = config.GetSection("Connection")["MSSQL"];
        }

      
        public IActionResult PcView(Games objGames)
        {
            var games = GetGames();
            return View(games);
        }
        public List<Games> GetGames()
        {
            List<Games> games = new List<Games>();
            var game = new Games(); 
            var query = "SELECT * FROM games";
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

        public IActionResult Ps4View()
        {
            return View();
        }
        public IActionResult XboxView()
        {
            return View();
        }
        public IActionResult NintendoView()
        {
            return View();
        }

    }
}