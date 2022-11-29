﻿namespace BO;
internal class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }
}
