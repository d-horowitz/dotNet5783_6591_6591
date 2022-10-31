using DO;
namespace Dal;

public class DalOrder
{
    public int Create(Order o)
    {
        o.Id = DataSource.Config.OrderId;
        DataSource._orders[DataSource.Config.OrderIndex] = o;
        return o.Id;
    }
    public Order Read(int orderId)
    {
        foreach (Order o in DataSource._orders)
        {
            if (o.Id == orderId) { return o; }
        }
        throw new Exception("Order not found");
    }
    public Order[] Read()
    {
        Order[] orders = new Order[DataSource.Config._orderIndex];
        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = DataSource._orders[i];
        }
        return orders;
    }
    public void Delete(int orderId)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config._orderIndex--; i++)
        {
            if (DataSource._orders[i].Id == orderId || found)
            {
                found = true;
                DataSource._orders[i] = DataSource._orders[i+1];
            }
        }
        if (!found)
        {
            DataSource.Config._orderIndex++;
            throw new Exception("Order not found");
        }
    }
    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.Config._orderIndex; i++)
        {
            if (DataSource._orders[i].Id==o.Id)
            {
                DataSource._orders[i] = o;
                return;
            }
        }
        throw new Exception("Order not found");
    }
}

