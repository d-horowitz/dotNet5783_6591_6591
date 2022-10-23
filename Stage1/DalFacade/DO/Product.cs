

namespace DO;
public struct Product
{
    int Id { get; set; }
    string Name { get; set; }
    ECategory Category { get; set; }
    double Price { get; set; }
    int Amount { get; set; }

    public override string ToString() => $@"
        Product ID={Id}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {Amount}
    ";
}
