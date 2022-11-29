namespace BO;
internal class OrderForList
{

    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public List<(DateTime, EOrderStatus)>? TrackList { get; set; }
}
