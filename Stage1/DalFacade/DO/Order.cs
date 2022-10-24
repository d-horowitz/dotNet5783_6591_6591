

namespace DO;
public struct Order
{
    int Id { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    string Address { get; set; }
    DateTime OrderCreated { get; set; }
    DateTime Shipping { get; set; }
    DateTime Delivery { get; set; }
    public override string ToString() => $@"
        OrderId={Id}: {Name},
        Email: {Email}, 
    	Address: {Address},
    	Order created on {OrderCreated}, shipped on {Shipping}, delivered on {Delivery}.}
    ";
}

