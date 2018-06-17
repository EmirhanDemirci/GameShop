using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShopDemirci.Models
{
    public class CartViewModel
    {
        public List<Games> Games { get;}
        public decimal TotalPrice { get;}

        public CartViewModel(List<Games> games, decimal totalPrice)
        {
            Games = games;
            TotalPrice = totalPrice;
        }
    }
}
