using BO;
namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList> Read();
    public Order Read(int orderId);
    public Order UpdateShipping(int OrderId);
    public Order UpdateDelivery(int OrderId);
}

