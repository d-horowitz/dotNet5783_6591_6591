namespace BO;
internal class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ECategory Category { get; set; }
    public int AmountInStock { get; set; }
}