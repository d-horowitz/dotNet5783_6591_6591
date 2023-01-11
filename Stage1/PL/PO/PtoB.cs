using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class PtoB
    {
        public static BO.Cart Cart(PoCart poCart)
        {
            return new()
            {
                CustomerAddress = poCart.CustomerAddress,
                CustomerEmail = poCart.CustomerEmail,
                CustomerName = poCart.CustomerName,
                Items = poCart.Items.ConvertAll(oi => new BO.OrderItem()
                {
                    Amount = oi.Amount,
                    Id = oi.Id,
                    Name = oi.Name,
                    Price = oi.Price,
                    ProductId = oi.ProductId,
                    TotalPrice = oi.TotalPrice
                }),
                TotalPrice = poCart.TotalPrice
            };
        }
    }
}
