using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class PtoB
    {
        public static BO.Cart ConvertCart(Cart cart)
        {
            return new()
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                CustomerName = cart.CustomerName,
                Items = cart.Items.ToList(),
                TotalPrice = cart.TotalPrice
            };
        }
    }
}
