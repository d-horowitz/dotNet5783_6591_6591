using DO;
namespace Dal;

internal static class DalOrderItem
{
    public static int Create(OrderItem oi)
    {
        oi.Id = DataSource.Config.OrderItemId;
        DataSource._orderItems[DataSource.Config.OrderItemIndex] = oi;
        return oi.Id;
    }
    public static OrderItem Read(int orderItemId)
    {
        foreach (OrderItem oi in DataSource._orderItems)
        {
            if (oi.Id == orderItemId) { return oi; }
        }
        throw new Exception("OrderItem not found");
    }
    public static OrderItem[] Read()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config._orderItemIndex];
        for (int i = 0; i < orderItems.Length; i++)
        {
            orderItems[i] = DataSource._orderItems[i];
        }
        return orderItems;
    }
    public static void Delete(int orderItemId)
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
    public static void Update(OrderItem oi)
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
}
