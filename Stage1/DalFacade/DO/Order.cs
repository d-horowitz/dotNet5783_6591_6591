

namespace DO;
public struct Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime OrderCreated { get; set; }
    public DateTime Shipping { get; set; }
    public DateTime Delivery { get; set; }
    public override string ToString() => $@"
        OrderId={Id},
        Customer name: {Name},
        Email: {Email}, 
    	Address: {Address},
    	Order created on {OrderCreated}, shipped on {Shipping}, delivered on {Delivery}";
}

