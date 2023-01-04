using BlApi;
using DalApi;
namespace BlImplementation;
internal class BlOrder : BlApi.IOrder
{
    private readonly IDal Dal = DalApi.Factory.Get();//DalList.Instance;
    public IEnumerable<BO.OrderForList> Read()
    {
        List<BO.OrderForList> OrderList = new();

        Dal.Order.Read().ToList().ForEach(o =>
        {
            OrderList.Add(
            new BO.OrderForList
            {
                Id = o.Id,
                CustomerName = o.Name,
                AmountOfItems = Dal.OrderItem.Read(oi => oi.OrderId == o.Id).Count(),
                OrderStatus = o.Shipping == DateTime.MinValue ? BO.EOrderStatus.Processed : o.Delivery == DateTime.MinValue ? BO.EOrderStatus.Shipped : BO.EOrderStatus.Delivered,
                TotalPrice = Dal.OrderItem.Read(oi => oi.OrderId == o.Id).Sum(oi => oi.UnitPrice * oi.Amount)
            }
            );
        });

        return OrderList;
        /*List<BO.OrderForList> OrdersList = new();
        try
        {
            IEnumerable<DO.Order> orders = Dal.Order.Read();
            foreach (var order in orders)
            {
                BO.OrderForList newOrder = new();
                newOrder.Id = order.Id;
                newOrder.CustomerName = order.Name;
                if (DateTime.Now <= order.Shipping)
                    newOrder.OrderStatus = BO.EOrderStatus.Processed;
                else if (DateTime.Now < order.Delivery)
                    newOrder.OrderStatus = BO.EOrderStatus.Shipped;
                else
                    newOrder.OrderStatus = BO.EOrderStatus.Delivered;
                int amount = 0;
                double price = 0;
                foreach (var orderItem in Dal.OrderItem.Read(oi => oi.orderId == order.Id))
                {
                    amount++;
                    price += orderItem.Amount * orderItem.UnitPrice;

                }
                newOrder.AmountOfItems = amount;
                newOrder.TotalPrice = price;
                OrdersList.Add(newOrder);
            }
        }
        catch (DataIsEmpty ex) { throw new BO.DataIsEmpty(ex); }
        return OrdersList;
        */
    }
    public BO.Order Read(int orderId)
    {
        if (orderId <= 0)
            throw new InvalidInput();
        try
        {
            DO.Order DOorder = Dal.Order.ReadSingle(order => order.Id == orderId);
            List<DO.OrderItem> orderItems = Dal.OrderItem.Read(oi => oi.OrderId == DOorder.Id).ToList();
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
                    Name = Dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
                    Price = oi.UnitPrice,
                    ProductId = oi.ProductId,
                    TotalPrice = oi.Amount * oi.UnitPrice
                })
            };
            return order;
            /*DO.Order DOorder = Dal.Order.ReadSingle(order => order.Id == orderId);
            BO.Order BOorder = new();
            BOorder.Id = orderId;
            BOorder.OrderCreated = DOorder.OrderCreated;
            BOorder.Shipping = DOorder.Shipping;
            BOorder.Delivery = DOorder.Delivery;
            BOorder.CustomerAddress = DOorder.Address;
            BOorder.CustomerEmail = DOorder.Email;
            BOorder.CustomerName = DOorder.Name;
            BOorder.TotalPrice = 0;
            List<BO.OrderItem> oiList = new();
            BO.OrderItem oi = new BO.OrderItem();
            foreach (var item in Dal.OrderItem.Read(orderItem => orderItem.orderId == orderId))
            {
                oi.Name = Dal.Product.ReadSingle(product => product.Id == item.ProductId).Name;
                oi.ProductId = item.ProductId;
                oi.Price = item.UnitPrice;
                oi.Id = item.Id;
                oi.Amount = item.Amount;
                oi.TotalPrice = item.Amount * item.UnitPrice;
                BOorder.TotalPrice += oi.TotalPrice;
                oiList.Add(oi);
            }
            BOorder.Items = oiList;
            return BOorder;*/
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
    public BO.Order UpdateShipping(int orderId)
    {
        try
        {
            DO.Order order = Dal.Order.ReadSingle(order => order.Id == orderId);
            if (order.Shipping == DateTime.MinValue)
            {
                order.Shipping = DateTime.Now;
                Dal.Order.Update(order);
                List<BO.OrderItem> orderItems = Dal.OrderItem.Read(oi => oi.OrderId == order.Id).ToList().ConvertAll<BO.OrderItem>(oi => new()
                {
                    Amount = oi.Amount,
                    Id = oi.Id,
                    Name = Dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
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
    public BO.Order UpdateDelivery(int orderId)
    {
        try
        {
            DO.Order order = Dal.Order.ReadSingle(order => order.Id == orderId);
            if (order.Delivery == DateTime.MinValue && order.Shipping != DateTime.MinValue)
            {
                order.Delivery = DateTime.Now;
                Dal.Order.Update(order);
                List<BO.OrderItem> orderItems = Dal.OrderItem.Read(oi => oi.OrderId == order.Id).ToList().ConvertAll<BO.OrderItem>(oi => new()
                {
                    Amount = oi.Amount,
                    Id = oi.Id,
                    Name = Dal.Product.ReadSingle(p => p.Id == oi.ProductId).Name,
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

    public BO.OrderTracking TrackOrder(int orderId)
    {
        try
        {
            DO.Order order = Dal.Order.ReadSingle(o => o.Id == orderId);
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
                ot.TrackList.Add((order.OrderCreated, BO.EOrderStatus.Processed));
                if (order.Shipping != DateTime.MinValue)
                {
                    ot.TrackList.Add((order.Shipping, BO.EOrderStatus.Shipped));
                    if (order.Delivery != DateTime.MinValue)
                        ot.TrackList.Add((order.Delivery, BO.EOrderStatus.Delivered));
                }
            }
            return ot;
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }
}