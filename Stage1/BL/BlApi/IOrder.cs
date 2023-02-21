using BO;
namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList> Read();
    public Order Read(int orderId);
    public Order UpdateShipping(int orderId);
    public Order UpdateDelivery(int orderId);
    public OrderTracking TrackOrder(int orderId);
    public int? NextOrder();
}

