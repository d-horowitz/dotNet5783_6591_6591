

namespace DO;
public struct Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ECategory Category { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        Product ID={Id}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {Amount}
    ";
    public bool Equals(Product p)
    {
        return p.Id == Id;
    }
}
