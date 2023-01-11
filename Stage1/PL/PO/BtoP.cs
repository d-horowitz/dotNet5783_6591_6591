using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class BtoP
    {
        public static PoCart Cart(BO.Cart cart)
        {
            return new()
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.ConvertAll(oi => new OrderItem()
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Name = oi.Name,
                    Price = oi.Price,
                    Amount = oi.Amount,
                    TotalPrice = oi.TotalPrice
                })
            };
        }
    }
}
