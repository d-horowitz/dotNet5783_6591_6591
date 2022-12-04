namespace BO;
public class Config
{
    private static int productId = 10000;
    public static int ProductId { get { return productId++; } }

    private static int orderId = 10000;
    public static int OrderId { get { return orderId++; } }

    private static int orderItemId = 10000;
    public static int OrderItemId { get { return orderItemId++; } }

    private static int Id = 10000;
    public static int ID { get { return Id++; } }

}
