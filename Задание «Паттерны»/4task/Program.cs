public interface IOrder
{
    double GetPrice();
    string GetDescription();
}

public class BaseOrder : IOrder
{
    public double BasePrice { get; set; }
    public string Description { get; set; }

    public BaseOrder(double basePrice, string description)
    {
        BasePrice = basePrice;
        Description = description;
    }

    public double GetPrice() => BasePrice;
    public string GetDescription() => Description;

}

public class OrderDecorator : IOrder
{
    protected IOrder order;

    public OrderDecorator(IOrder order)
    {
        this.order = order;
    }

    public virtual double GetPrice() => order.GetPrice();
    public virtual string GetDescription() => order.GetDescription();
}

public class ExpressDelivery : OrderDecorator
{
    public ExpressDelivery(IOrder order) : base(order) { }
    public override double GetPrice() => order.GetPrice() + 30;
    public override string GetDescription() => $"{order.GetDescription()}, Express Delivery";
}

public class GiftWrapping : OrderDecorator
{
    public GiftWrapping(IOrder order) : base(order) { }
    public override double GetPrice() => order.GetPrice() + 20;
    public override string GetDescription() => $"{order.GetDescription()}, Gift Wrapping";
}

public class Drinks : OrderDecorator
{
    public Drinks(IOrder order) : base(order) { }
    public override double GetPrice() => order.GetPrice() + 15;
    public override string GetDescription() => $"{order.GetDescription()}, Drinks";
}

public class Example
{
    public static void Main(string[] args)
    {
        BaseOrder baseOrder = new BaseOrder(100, "Pizza");
        Console.WriteLine($"Base order: {baseOrder.GetDescription()}, Price: {baseOrder.GetPrice()}");

        IOrder orderWithDelivery = new ExpressDelivery(baseOrder);
        Console.WriteLine($"Order with delivery: {orderWithDelivery.GetDescription()}, Price: {orderWithDelivery.GetPrice()}");

        IOrder orderWithDeliveryAndGift = new GiftWrapping(orderWithDelivery);
        Console.WriteLine($"Order with delivery and gift: {orderWithDeliveryAndGift.GetDescription()}, Price: {orderWithDeliveryAndGift.GetPrice()}");

        IOrder finalOrder = new Drinks(orderWithDeliveryAndGift);
        Console.WriteLine($"Final order: {finalOrder.GetDescription()}, Price: {finalOrder.GetPrice()}");
    }
}
