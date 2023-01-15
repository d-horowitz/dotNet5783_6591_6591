using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
internal class Xproduct : XElement
{
    public Xproduct(DO.Product p) : base("Product",
        new XElement("Id", p.Id),
        new XElement("Name", p.Name),
        new XElement("Category", p.Category),
        new XElement("Price", p.Price),
        new XElement("Amount", p.Amount))
    {
    }
}
internal class XOrder : XElement
{
    public XOrder(DO.Order o) : base("Order",
        new XElement("Id", o.Id),
        new XElement("Name", o.Name),
        new XElement("Email", o.Email),
        new XElement("Address", o.Address),
        new XElement("OrderCreated", o.OrderCreated),
        new XElement("Shipping", o.Shipping),
        new XElement("Delivery", o.Delivery)
        )
    {
    }
}
internal class XOrderItem : XElement
{
    public XOrderItem(DO.OrderItem oi) : base("OrderItem",
        new XElement("Id", oi.Id),
        new XElement("OrderId", oi.OrderId),
        new XElement("ProductId", oi.ProductId),
        new XElement("UnitPrice", oi.UnitPrice),
        new XElement("Amount", oi.Amount)
        )
    {
    }
}
