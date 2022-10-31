﻿using DO;
namespace Dal;

public class DataSource
{
    public static readonly Random _randomer = new Random();
    public static Product[] _products = new Product[50];
    public static Order[] _orders = new Order[100];
    public static OrderItem[] _orderItems = new OrderItem[200];

    public static class Config
    {
        public static int _productIndex = 0;
        public static int _orderIndex = 0;
        public static int _orderItemIndex = 0;
        public static int _productId = 100000;
        public static int _orderId = 100000;
        public static int _orderItemId = 100000;
        public static int ProductIndex { get { return _productIndex++; } }
        public static int OrderIndex { get { return _orderIndex++; } }
        public static int OrderItemIndex { get { return _orderItemIndex++; } }
        public static int ProductId { get { return _productId++; } }
        public static int OrderId { get { return _orderId++; } }
        public static int OrderItemId { get { return _orderItemId++; } }
    }
    private static void addProducts()
    {
        for (int i = 0; i < 10; i++)
        {
            Product p = new()
            {
                Id = Config.ProductId,
                Name = "BOOK " + (char)(i + 65),
                Category = (ECategory)(i % 5),
                Price = 90 + i * 1.1,
                Amount = i
            };
            _products[Config.ProductIndex] = p;
        }
    }
    private static void addOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            Order o = new()
            {
                Id = Config.OrderId,
                Name = "Customer " + (char)(i + 97),
                Email = "customer" + (char)(i + 97) + "@gmail.com",
                Address = i + " " + (char)(i + 65) + " st.",
                OrderCreated = DateTime.Now - TimeSpan.FromDays(_randomer.NextInt64(10, 20))
            };
            o.Shipping = i%10<8 ? o.OrderCreated+TimeSpan.FromDays(_randomer.NextInt64(3, 5)) : DateTime.MinValue;
            o.Delivery = i%10<5 ? o.Shipping+TimeSpan.FromDays(_randomer.NextInt64(1, 2)) : DateTime.MinValue;
            _orders[Config.OrderIndex] = o;
        }
    }
    private static void addOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            OrderItem oi = new()
            {
                Id = Config.OrderItemId,
                OrderId = _orders[_randomer.NextInt64(0, _orders.Length - 1)].Id,
                ProductId = _products[_randomer.NextInt64(0, _products.Length)].Id,
                UnitPrice = _randomer.NextDouble()*75+55,
                Amount = (int)_randomer.NextInt64(1, 6)
            };
            _orderItems[Config.OrderItemIndex] = oi;
        }
    }
    private static void s_Initialize()
    {
        addProducts();
        addOrders();
        addOrderItems();
    }
    // Constructor:
    static DataSource() => s_Initialize();
};

