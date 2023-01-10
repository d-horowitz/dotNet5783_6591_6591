using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class BtoP
    {
        public static Cart ConvertCart(BO.Cart cart)
        {
            ObservableCollection<BO.OrderItem> list = new();
            cart.Items.ForEach(x => list.Add(x));
            return new()
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                CustomerName = cart.CustomerName,
                Items = list,
                TotalPrice = cart.TotalPrice
            };
        }
    }
}
