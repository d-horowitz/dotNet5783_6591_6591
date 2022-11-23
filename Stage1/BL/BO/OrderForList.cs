namespace BO;
internal class OrderForList
{
    public int id { get; set; }
    public string? CustomerName { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
}
