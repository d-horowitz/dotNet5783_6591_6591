namespace BO;
internal class ProductForList
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ECategory Category { get; set; }
    public override string ToString() => $@"
        Product ID={Id}: {Name}, 
        category - {Category} 
        Price: {Price} 
    ";
}
