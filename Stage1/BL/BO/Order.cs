namespace BO;
public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public EOrderStatus Status { get; set; }
    public DateTime OrderCreated { get; set; }
    public DateTime Shipping { get; set; }
    public DateTime Delivery { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string text =
            $@"order ID={Id},
            customer mame: {CustomerName}, 
            email {CustomerEmail}, 
            address {CustomerAddress}.
            order date: {OrderCreated}, 
            ship date: {Shipping}, 
            delivery date: {Delivery}, 
            status: {Status}.
            total price:{TotalPrice}
            items:";
        foreach (var i in Items) { text += "\n \t " + i; };
        return text;
    }
}
