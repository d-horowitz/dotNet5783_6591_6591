namespace BO;
public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
        Order ID={Id}: {Name},
        ProductID - {ProductId} 
        Price: {Price} 
        Amount: {Amount} 
        TotalPrice: {TotalPrice}
    ";
}
