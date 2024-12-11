public interface IChatMediator
{
    void RegisterChatMember(IChatMember member);
    void SendMessage(string message, IChatMember sender);
}

public interface IChatMember
{
    void ReceiveMessage(string message, IChatMember sender);
    string Name { get; }
}

public class ChatMediator : IChatMediator
{
    private List<IChatMember> members = new List<IChatMember>();

    public void RegisterChatMember(IChatMember member)
    {
        members.Add(member);
    }

    public void SendMessage(string message, IChatMember sender)
    {
        foreach (var member in members)
        {
            if (member != sender)
            {
                member.ReceiveMessage(message, sender);
            }
        }
    }
}

public class ChatMember : IChatMember
{
    public string Name { get; }
    private IChatMediator mediator;

    public ChatMember(string name, IChatMediator mediator)
    {
        Name = name;
        this.mediator = mediator;
    }

    public void ReceiveMessage(string message, IChatMember sender)
    {
        Console.WriteLine($"{Name}: Received message from {sender.Name}: {message}");
    }

    public void SendMessage(string message)
    {
        mediator.SendMessage(message, this);
    }
}


public class Example
{
    public static void Main(string[] args)
    {
        ChatMediator mediator = new ChatMediator();
        ChatMember member1 = new ChatMember("User1", mediator);
        ChatMember member2 = new ChatMember("User2", mediator);
        ChatMember member3 = new ChatMember("User3", mediator);

        mediator.RegisterChatMember(member1);
        mediator.RegisterChatMember(member2);
        mediator.RegisterChatMember(member3);

        member1.SendMessage("Hello everyone!");
        member2.SendMessage("Hi!");
    }
}
