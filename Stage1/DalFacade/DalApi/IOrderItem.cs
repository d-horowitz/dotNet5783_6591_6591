using DO;
namespace DalApi;
public interface IOrderItem: ICrud<OrderItem>
{
    public OrderItem Read(int orderId, int productId);
    public IEnumerable<OrderItem> ReadList(int orderId);
}
