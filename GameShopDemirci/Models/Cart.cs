using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GameShopDemirci.Models
{

    //static = hoeft niet te initialise
    //classes nooit meervoud
    public class Cart
    {
        readonly string _connectionString;
        public static List<Games> Games { get; } = new List<Games>();

        public Cart(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddGame(int id)
        {
            var game = new Games();
            var query = "SELECT * FROM games WHERE gameId =" + id;
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
                    Games.Add(game);
                }
            }
        }
    }
}
