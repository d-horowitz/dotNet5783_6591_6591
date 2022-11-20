
namespace DO;

public struct OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double UnitPrice { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        OrderItem ID={Id},
        OrderId={OrderId},
        Product ID={ProductId}, 
    	UnitPrice: {UnitPrice},
    	Amount of units: {Amount}
    ";
    public bool Equals(OrderItem oi)
    {
        return oi.Id == Id;
    }
}
