namespace BO;
public class OrderTracking
{
    public int Id { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public List<Tuple<DateTime, EOrderStatus>>? TrackList { get; set; }
    public override string ToString() => $@"
        Order ID={Id}: 
        Status - {OrderStatus}, 
    ";
}