using BlApi;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlOrder : BlApi.IOrder
{
    private readonly IDal dal = DalApi.Factory.Get() ?? throw new Exception("Failed to load Dal");//DalList.Instance;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> Read()
    {
        lock (dal)
        {
            return from o in (
            from o in dal.Order.Read()
            select new BO.OrderForList()
            {
                Id = o.Id,
                CustomerName = o.Name,
                AmountOfItems = dal.OrderItem.Read(oi => oi.OrderId == o.Id).Count(),
                OrderStatus = o.Shipping == DateTime.MinValue ? BO.EOrderStatus.Processed : o.Delivery == DateTime.MinValue ? BO.EOrderStatus.Shipped : BO.EOrderStatus.Delivered,
                TotalPrice = dal.OrderItem.Read(oi => oi.OrderId == o.Id).Sum(oi => oi.UnitPrice * oi.Amount)
            })
                   orderby o.OrderStatus, o.CustomerName
                   select o;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order Read(int orderId)
    {
        if (orderId <= 0)
            throw new InvalidInput();
        try
        {
            lock (dal)
            {
                DO.Order DOorder = dal.Order.ReadSingle(order => order.Id == orderId);
                List<DO.OrderItem> orderItems = dal.OrderItem.Read(oi => oi.OrderId == DOorder.Id).ToList();
                BO.Order order = new()
                {
                    Id = DOorder.Id,
                    CustomerName = DOorder.Name,
                    CustomerAddress = DOorder.Address,
                    CustomerEmail = DOorder.Email,
                    OrderCreated = DOorder.OrderCreated,
                    Shipping = DOorder.Shipping,
                    Delivery = DOorder.Delivery,
                    TotalPrice = orderItems.Sum(oi => oi.UnitPrice * oi.Amount),
                    Status = DOorder.Shipping == DateTime.MinValue ? BO.EOrderStatus.Processed : DOorder.Delivery == DateTime.MinValue ? BO.EOrderStatus.Shipped : BO.EOrderStatus.Delivered,
                    Items = orderItems.ConvertAll<BO.OrderItem>(oi => new()
                    {
                        Amount = oi.Amount,
                        Id = oi.Id,
                        Name = dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
                        Price = oi.UnitPrice,
                        ProductId = oi.ProductId,
                        TotalPrice = oi.Amount * oi.UnitPrice
                    })
                };
                return order;
            }
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateShipping(int orderId)
    {
        try
        {
            lock (dal)
            {
                DO.Order order = dal.Order.ReadSingle(order => order.Id == orderId);
                if (order.Shipping == DateTime.MinValue)
                {
                    order.Shipping = DateTime.Now;
                    dal.Order.Update(order);
                    List<BO.OrderItem> orderItems = dal.OrderItem.Read(oi => oi.OrderId == order.Id).ToList().ConvertAll<BO.OrderItem>(oi => new()
                    {
                        Amount = oi.Amount,
                        Id = oi.Id,
                        Name = dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
                        Price = oi.UnitPrice,
                        ProductId = oi.ProductId,
                        TotalPrice = oi.Amount * oi.UnitPrice
                    });

                    return new()
                    {
                        Id = order.Id,
                        CustomerName = order.Name,
                        CustomerAddress = order.Address,
                        CustomerEmail = order.Email,
                        OrderCreated = order.OrderCreated,
                        Shipping = DateTime.Now,
                        Delivery = order.Delivery,
                        Items = orderItems,
                        Status = BO.EOrderStatus.Shipped,
                        TotalPrice = orderItems.Sum(oi => oi.TotalPrice)
                    };
                }
                else
                {
                    throw new InvalidInput();
                }
            }
            throw new NonExistentObject();
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateDelivery(int orderId)
    {
        try
        {
            lock (dal)
            {
                DO.Order order = dal.Order.ReadSingle(order => order.Id == orderId);
                if (order.Delivery == DateTime.MinValue && order.Shipping != DateTime.MinValue)
                {
                    order.Delivery = DateTime.Now;
                    dal.Order.Update(order);
                    List<BO.OrderItem> orderItems = dal.OrderItem.Read(oi => oi.OrderId == order.Id).ToList().ConvertAll<BO.OrderItem>(oi => new()
                    {
                        Amount = oi.Amount,
                        Id = oi.Id,
                        Name = dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
                        Price = oi.UnitPrice,
                        ProductId = oi.ProductId,
                        TotalPrice = oi.Amount * oi.UnitPrice
                    });

                    return new()
                    {
                        Id = order.Id,
                        CustomerName = order.Name,
                        CustomerAddress = order.Address,
                        CustomerEmail = order.Email,
                        OrderCreated = order.OrderCreated,
                        Shipping = order.Shipping,
                        Delivery = DateTime.Now,
                        Items = orderItems,
                        Status = BO.EOrderStatus.Delivered,
                        TotalPrice = orderItems.Sum(oi => oi.TotalPrice)
                    };
                }
                else
                {
                    throw new InvalidInput();
                }
            }
            throw new NonExistentObject();
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking TrackOrder(int orderId)
    {
        try
        {
            lock (dal)
            {
                DO.Order order = dal.Order.ReadSingle(o => o.Id == orderId);
                if (order.Id == default)
                {
                    throw new NonExistentObject();
                }
                BO.OrderTracking ot = new()
                {
                    Id = order.Id,
                    OrderStatus = order.Shipping == DateTime.MinValue ? BO.EOrderStatus.Processed : order.Delivery == DateTime.MinValue ? BO.EOrderStatus.Shipped : BO.EOrderStatus.Delivered,
                    TrackList = new()
                };
                if (order.OrderCreated != DateTime.MinValue)
                {
                    ot.TrackList.Add(new Tuple<DateTime, BO.EOrderStatus>(order.OrderCreated, BO.EOrderStatus.Processed));
                    if (order.Shipping != DateTime.MinValue)
                    {
                        ot.TrackList.Add(new Tuple<DateTime, BO.EOrderStatus>(order.Shipping, BO.EOrderStatus.Shipped));
                        if (order.Delivery != DateTime.MinValue)
                            ot.TrackList.Add(new Tuple<DateTime, BO.EOrderStatus>(order.Delivery, BO.EOrderStatus.Delivered));
                    }
                }
                return ot;
            }
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? NextOrder()
    {
        int id;
        lock (dal)
        {
            id = (from o in (
                      from order in dal.Order.Read()
                      where order.Delivery == DateTime.MinValue
                      select order
                      )
                  let lastUpdate = new[] { o.OrderCreated, o.Shipping }.Max()
                  orderby lastUpdate
                  select o.Id).FirstOrDefault((int)default);
        }
        return id == default ? null : id;
    }
}