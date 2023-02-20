using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem oi)
    {
        oi.Id = DataSource.Config.OrderItemId;
        DataSource._orderItems.Add(oi);
        return oi.Id;
    }
    /*
    public OrderItem Read(int orderItemId)
    {
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.Id == orderItemId) { return oi; }
        }
        throw new Exception("Order item not found");
    }
    */

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        Predicate<OrderItem> myFunc = new(func);
        return DataSource._orderItems.Find(myFunc);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> Read(Func<OrderItem, bool>? func = null)
    {
        IEnumerable<OrderItem> orderItems;
        if (func != null)
            orderItems = new List<OrderItem>(DataSource._orderItems.Where(func));
        else
            orderItems = new List<OrderItem>(DataSource._orderItems);
        return orderItems;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int orderItemId)
    {
        if (!DataSource._orderItems.Remove(new OrderItem { Id = orderItemId }))
            throw new Exception("Order item not found");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < DataSource._orderItems.Count; i++)
        {
            if (DataSource._orderItems[i].Id == oi.Id)
            {
                DataSource._orderItems[i] = oi;
                return;
            }
        }
        throw new Exception("Order item not found");
    }
    /*
    public OrderItem Read(int orderId, int productId)
    {
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.ProductId == productId && oi.OrderId == orderId)
            {
                return oi;
            }
        }
        throw new Exception("Order item not found");
    }
   
    public IEnumerable<OrderItem> ReadList(int orderId)
    {
        IEnumerable<OrderItem> orderItems = DataSource._orderItems.FindAll(oi => oi.OrderId == orderId);
        return orderItems;
    }
    */
}
