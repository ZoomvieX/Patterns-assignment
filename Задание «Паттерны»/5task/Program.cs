public interface IService
{
    string GetData();
}

public class RealService : IService
{
    public string GetData()
    {
        Console.WriteLine("Calling RealService...");
        Thread.Sleep(3000); 
        return "Data from RealService";
    }
}

public class ProxyService : IService
{
    private IService realService;
    private string cachedData;
    private DateTime lastCallTime;

    public ProxyService(IService realService)
    {
        this.realService = realService;
    }

    public string GetData()
    {
        TimeSpan timeSinceLastCall = DateTime.Now - lastCallTime;

        if (timeSinceLastCall < TimeSpan.FromSeconds(5) && cachedData != null)
        {
            Console.WriteLine("Returning cached data...");
            return cachedData;
        }
        else
        {
            cachedData = realService.GetData();
            lastCallTime = DateTime.Now;
            Console.WriteLine("Data cached.");
            return cachedData;
        }
    }
}

public class Example5
{
    public static void Main(string[] args)
    {
        RealService realService = new RealService();
        ProxyService proxyService = new ProxyService(realService);

        Console.WriteLine($"First call: {proxyService.GetData()}");
        Console.WriteLine($"Second call (within 5 seconds): {proxyService.GetData()}");
        Thread.Sleep(6000); 
        Console.WriteLine($"Third call (after 5 seconds): {proxyService.GetData()}");
    }
}
