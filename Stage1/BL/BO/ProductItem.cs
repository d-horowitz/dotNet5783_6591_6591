namespace BO;
internal class ProductItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ECategory Category { get; set; }
    public bool InStock { get; set; }
    public int Amount { get; set; }
}