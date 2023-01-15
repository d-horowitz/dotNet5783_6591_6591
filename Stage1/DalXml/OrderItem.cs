using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;
internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem oi)
    {
        XElement? config = XDocument.Load(@"..\..\xml\config.xml").Root;
        XElement? orderItemId = config?.Element("OrderItemId");
        oi.Id = Convert.ToInt32(orderItemId?.Value ?? "");
        orderItemId.Value = (oi.Id + 1).ToString();
        config?.Save(@"..\..\xml\config.xml");
        XElement? root = XDocument.Load(@"..\..\xml\OrderItem.xml").Root;
        root?.Add(new XOrderItem(oi));
        root?.Save(@"..\..\xml\OrderItem.xml");
        return oi.Id;
    }

    public void Delete(int id)
    {

    }

    public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool>? func = null)
    {
        XElement? root = XDocument.Load(@"..\..\xml\OrderItem.xml").Root;
        List<XElement> xorderItems = root?.Elements("OrderItem").ToList() ?? new();
        List<DO.OrderItem> orderItems = new();
        for (int i = 0; i < xorderItems.Count; i++)
        {
            orderItems.Add(new DO.OrderItem()
            {
                Id = Convert.ToInt32(xorderItems[i]?.Element("Id")?.Value),
                OrderId = Convert.ToInt32(xorderItems[i]?.Element("OrderId")?.Value),
                ProductId = Convert.ToInt32(xorderItems[i]?.Element("ProductId")?.Value),
                UnitPrice = Convert.ToDouble(xorderItems[i]?.Element("UnitPrice")?.Value),
                Amount = Convert.ToInt32(xorderItems[i]?.Element("Amount")?.Value)
            });
        }
        if (func == null)
            return orderItems;
        return from oi in orderItems
               where func(oi)
               select oi;
    }

    public DO.OrderItem ReadSingle(Func<DO.OrderItem, bool> func)
    {

        XElement? root = XDocument.Load(@"..\..\xml\OrderItem.xml").Root;
        List<XElement> xorderItems = root?.Elements("OrderItem").ToList() ?? new();
        List<DO.OrderItem> orderItems = new();
        for (int i = 0; i < xorderItems.Count; i++)
        {
            orderItems.Add(new DO.OrderItem()
            {
                Id = Convert.ToInt32(xorderItems[i]?.Element("Id")?.Value),
                OrderId = Convert.ToInt32(xorderItems[i]?.Element("OrderId")?.Value),
                ProductId = Convert.ToInt32(xorderItems[i]?.Element("ProductId")?.Value),
                UnitPrice = Convert.ToDouble(xorderItems[i]?.Element("UnitPrice")?.Value),
                Amount = Convert.ToInt32(xorderItems[i]?.Element("Amount")?.Value)
            });
        }
        return (from oi in orderItems
               where func(oi)
               select oi).First();
    }

    public void Update(DO.OrderItem item)
    {

    }
}