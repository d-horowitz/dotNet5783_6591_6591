﻿namespace BO;
public class ProductItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ECategory Category { get; set; }
    public bool InStock { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
        Product ID={Id}: {Name}, 
        category - {Category} 
        Price: {Price} 
        Amount: {Amount} 
        Amount in stock: {InStock}
    ";
}