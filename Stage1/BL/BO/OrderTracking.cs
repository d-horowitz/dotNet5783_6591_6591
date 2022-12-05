namespace BO;
public class OrderTracking
{
    public int Id { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public override string ToString() => $@"
        Order ID={Id}: 
        Status - {OrderStatus}, 
    ";
}