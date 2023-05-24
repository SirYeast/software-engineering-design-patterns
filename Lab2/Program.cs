Client user = new User();
Console.WriteLine(user.GetReputation());
user.GetPriveleges();

user = new CommunityBadge(user);
Console.WriteLine(user.GetReputation());
user.GetPriveleges();

user = new HundredPostsBadge(user);
Console.WriteLine(user.GetReputation());
user.GetPriveleges();

user = new BannedBadge(user);
Console.WriteLine(user.GetReputation());
user.GetPriveleges();

abstract class Client
{
    public abstract int GetReputation();
    public abstract void GetPriveleges();
}

class User : Client
{
    private int _reputation;

    public User() { }

    private void _grantBasicAccess()
    {
        Console.WriteLine("User now has basic access.");
    }

    public override int GetReputation()
    {
        return _reputation;
    }

    public override void GetPriveleges()
    {
        _grantBasicAccess();
    }
}

abstract class Badges : Client
{
    protected Client DecoratedClient;

    public Badges(Client client)
    {
        DecoratedClient = client;
    }

    public abstract override int GetReputation();
    public abstract override void GetPriveleges();
}

class CommunityBadge : Badges
{
    public CommunityBadge(Client client) : base(client) { }

    private void _grantGroupAccess()
    {
        Console.WriteLine("Group access has been granted.");
    }

    public override int GetReputation()
    {
        return DecoratedClient.GetReputation() + 5;
    }

    public override void GetPriveleges()
    {
        _grantGroupAccess();
    }
}

class BannedBadge : Badges
{
    public BannedBadge(Client client) : base(client) { }

    private void _blockAccess()
    {
        Console.WriteLine("Access has been blocked.");
    }

    public override int GetReputation()
    {
        return DecoratedClient.GetReputation() * 0;
    }

    public override void GetPriveleges()
    {
        _blockAccess();
    }
}

class HundredPostsBadge : Badges
{
    public HundredPostsBadge(Client client) : base(client) { }

    public override int GetReputation()
    {
        return DecoratedClient.GetReputation() + 100;
    }

    public override void GetPriveleges()
    {
        DecoratedClient.GetPriveleges();
    }
}