User user = new("Bob", "bob@email.com", 24)
{
    Reputation = 18
};

Console.WriteLine("User:");
Console.WriteLine($"Reputation: {user.Reputation} | Has Access: {user.HandleAccess()}");
Console.WriteLine($"Reputation: {user.Reputation = 21} | Has Access: {user.HandleAccess()}\n");

Manager manager = new("Gerald", "theG@email.com", 37)
{
    AccessDisabled = true
};

Console.WriteLine("Manager:");
Console.WriteLine($"Access Disabled: {manager.AccessDisabled} | Has Access: {manager.HandleAccess()}");
Console.WriteLine($"Access Disabled: {manager.AccessDisabled = false} | Has Access: {manager.HandleAccess()}");

interface IAccessHandler
{
    bool GetAccess(int reputation = 0, bool accessDisabled = false);
}

class HasReputation : IAccessHandler
{
    public bool GetAccess(int reputation = 0, bool accessDisabled = false)
    {
        return !accessDisabled && reputation > 20;
    }
}

class HasAutomaticAccess : IAccessHandler
{
    public bool GetAccess(int reputation = 0, bool accessDisabled = false)
    {
        return !accessDisabled;
    }
}

abstract class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    protected IAccessHandler accessHandler;
    public bool AccessDisabled { get; set; } = false;

    public Client(string name, string email, int age, IAccessHandler access)
    {
        Name = name;
        Email = email;
        Age = age;

        accessHandler = access;
    }

    public virtual bool HandleAccess()
    {
        return accessHandler.GetAccess(0, AccessDisabled);
    }
}

class User : Client
{
    public int Reputation { get; set; }

    public User(string name, string email, int age) : base(name, email, age, new HasReputation())
    {
    }

    public override bool HandleAccess()
    {
        return accessHandler.GetAccess(Reputation, AccessDisabled);
    }
}

class Manager : Client
{
    public Manager(string name, string email, int age) : base(name, email, age, new HasAutomaticAccess())
    {
    }
}

class Admin : Client
{
    public Admin(string name, string email, int age) : base(name, email, age, new HasAutomaticAccess())
    {
    }
}