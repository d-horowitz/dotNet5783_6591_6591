using DO;
namespace Dal;

public class DalOrderItem
{
    public int Create(OrderItem oi)
    {
        oi.Id = DataSource.Config.OrderItemId;
        DataSource._orderItems[DataSource.Config.OrderItemIndex] = oi;
        return oi.Id;
    }
    public OrderItem Read(int orderItemId)
    {
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.Id == orderItemId) { return oi; }
        }
        throw new Exception("OrderItem not found");
    }
    public OrderItem[] Read()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config._orderItemIndex];
        for (int i = 0; i < orderItems.Length; i++)
        {
            orderItems[i] = DataSource._orderItems[i];
        }
        return orderItems;
    }
    public void Delete(int orderItemId)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config._orderItemIndex--; i++)
        {
            if (DataSource._orderItems[i].Id == orderItemId || found)
            {
                found = true;
                DataSource._orderItems[i] = DataSource._orderItems[i+1];
            }
        }
        if (!found)
        {
            DataSource.Config._orderItemIndex++;
            throw new Exception("OrderItem not found");
        }
    }
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < DataSource.Config._orderItemIndex; i++)
        {
            if (DataSource._orderItems[i].Id==oi.Id)
            {
                DataSource._orderItems[i] = oi;
                return;
            }
        }
        throw new Exception("OrderItem not found");
    }
    public OrderItem Read(int productId, int orderId)
    {
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.ProductId == productId && oi.OrderId == orderId)
            {
                return oi;
            }
        }
        throw new Exception("OrderItem not found");
    }
    public OrderItem[] ReadList(int orderId)
    {
        int number = 0;
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.OrderId == orderId) { number++; }
        }
        if (number==0) { throw new Exception("Items not found"); }
        OrderItem[] orderItems = new OrderItem[number];
        for (int i = 0; i < DataSource.Config._orderItemIndex; i++)
        {
            if (DataSource._orderItems[i].OrderId == orderId)
            {
                orderItems[i] = DataSource._orderItems[i];
            }
        }
        return orderItems;
    }
}
