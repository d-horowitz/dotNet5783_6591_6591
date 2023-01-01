using DalApi;
using DO;

namespace Dal;
sealed public class DalXml : IDal
{
    private DalXml()
    {

    }
    public static IDal Instance { get; } = new DalXml();
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}