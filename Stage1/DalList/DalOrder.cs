using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order o)
    {
        o.Id = DataSource.Config.OrderId;
        o.OrderCreated = DateTime.Now;
        o.Shipping = DateTime.MinValue;
        o.Delivery = DateTime.MinValue;
        DataSource._orders.Add(o);
        return o.Id;
    }
    /*
    public Order Read(int orderId)
    {
        foreach (Order o in DataSource._orders)
        {
            if (o.Id == orderId) { return o; }
        }
        throw new Exception("Order not found");
    }
    */

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order ReadSingle(Func<Order, bool> func)
    {
        Predicate<Order> myFunc = new(func);
        return DataSource._orders.Find(myFunc);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> Read(Func<Order, bool>? func = null)
    {
        IEnumerable<Order> orders;
        if (func != null)
            orders = new List<Order>(DataSource._orders.Where(func));
        else
            orders = new List<Order>(DataSource._orders);
        return orders;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int orderId)
    {
        if (!DataSource._orders.Remove(new Order { Id = orderId }))
            throw new Exception("Order not found");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order o)
    {
        for (int i = 0; i < DataSource._orders.Count; i++)
        {
            if (DataSource._orders[i].Id == o.Id)
            {
                DataSource._orders[i] = o;
                return;
            }
        }
        throw new Exception("Order not found");
    }
}

