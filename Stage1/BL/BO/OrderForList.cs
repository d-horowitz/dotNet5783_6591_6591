namespace BO;
public class OrderForList
{

    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
        Order ID={Id}: {CustomerName},
        Status: {OrderStatus} 
        AmountOfItems: {AmountOfItems} 
        TotalPrice: {TotalPrice}
    ";
}
