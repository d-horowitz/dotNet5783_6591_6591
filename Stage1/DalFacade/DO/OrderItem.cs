
namespace DO;

public struct OrderItem
{
    int Id { get; set; }
    int OrderId { get; set; }
    int ProductId { get; set; }
    double UnitPrice { get; set; }
    int Amount { get; set; }

    public override string ToString() => $@"
        OrderItem ID={Id},
        OrderId={OrderId},
        Product ID={ProductId}, 
    	UnitPrice: {UnitPrice},
    	Amount of units: {Amount}
    ";
}
