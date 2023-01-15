using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;
internal class Order : IOrder
{
    public int Add(DO.Order o)
    {
        XElement? config = XDocument.Load(@"..\..\xml\config.xml").Root;
        XElement? orderId = config?.Element("OrderId");
        o.Id = Convert.ToInt32(orderId?.Value ?? "");
        orderId.Value = (o.Id + 1).ToString();
        config?.Save(@"..\..\xml\config.xml");
        XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
        root?.Add(
            new XOrder(o)
        );
        root?.Save(@"..\..\xml\Order.xml");
        return o.Id;
    }
    public DO.Order ReadSingle(Func<DO.Order, bool> func)
    {
        XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
        List<XElement> xorders = root?.Elements("Order").ToList() ?? new();
        List<DO.Order> orders = new();
        for (int i = 0; i < xorders.Count; i++)
        {
            orders.Add(new DO.Order()
            {
                Id = Convert.ToInt32(xorders[i]?.Element("Id")?.Value),
                Name = xorders[i]?.Element("Name")?.Value,
                Email = xorders[i]?.Element("Email")?.Value,
                Address = xorders[i]?.Element("Address")?.Value,
                OrderCreated = Convert.ToDateTime(xorders[i]?.Element("OrderCreated")?.Value),
                Shipping = Convert.ToDateTime(xorders[i]?.Element("Shipping")?.Value),
                Delivery = Convert.ToDateTime(xorders[i]?.Element("Delivery")?.Value)
            });
        }
        return (from o in orders
                where func(o)
                select o).First();
    }
    public IEnumerable<DO.Order> Read(Func<DO.Order, bool>? func = null)
    {
        XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
        List<XElement> xorders = root?.Elements("Order").ToList() ?? new();
        List<DO.Order> orders = new();
        for (int i = 0; i < xorders.Count; i++)
        {
            orders.Add(new DO.Order()
            {
                Id = Convert.ToInt32(xorders[i]?.Element("Id")?.Value),
                Name = xorders[i]?.Element("Name")?.Value,
                Email = xorders[i]?.Element("Email")?.Value,
                Address = xorders[i]?.Element("Address")?.Value,
                OrderCreated = Convert.ToDateTime(xorders[i]?.Element("OrderCreated")?.Value),
                Shipping = Convert.ToDateTime(xorders[i]?.Element("Shipping")?.Value),
                Delivery = Convert.ToDateTime(xorders[i]?.Element("Delivery")?.Value)
            });
        }
        if (func == null)
            return orders;
        return from o in orders
               where func(o)
               select o;
    }
    public void Delete(int id)
    {

    }
    public void Update(DO.Order o)
    {
        XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
        XElement xorder = (from or in root?.Elements("Order")
                           where or.Element("Id")?.Value == o.Id.ToString()
                           select or).First() ?? throw new Exception("Order not found");
        xorder.Element("Name").Value = o.Name ?? "";
        xorder.Element("Email").Value = o.Email ?? "";
        xorder.Element("Address").Value = o.Address ?? "";
        xorder.Element("OrderCreated").Value = o.OrderCreated.ToString();
        xorder.Element("Shipping").Value = o.Shipping.ToString();
        xorder.Element("Delivery").Value = o.Delivery.ToString();
        root?.Save(@"..\..\xml\Order.xml");
    }
}