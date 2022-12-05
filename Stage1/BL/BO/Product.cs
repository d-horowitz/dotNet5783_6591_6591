namespace BO;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ECategory Category { get; set; }
    public int AmountInStock { get; set; }
    public override string ToString() => $@"
        Product ID={Id}: {Name}, 
        category - {Category} 
        Price: {Price} 
        Amount in stock: {AmountInStock}
    ";
}