User user1 = new()
{
    Password = "password",
    TwoFactorAuthentication = false,
    TwoFactorNotRequired = true
};

User user2 = new()
{
    Password = "password",
    TwoFactorAuthentication = false,
    TwoFactorNotRequired = true,
    IsAdmin = true
};

AuthorizedUserFactory authorizedUserFactory = new();
user1 = authorizedUserFactory.Initialize(user1);

AdminFactory adminFactory = new();
user2 = adminFactory.Initialize(user2);

class User
{
    public string Password { get; set; }

    public bool TwoFactorAuthentication { get; set; }
    public bool TwoFactorNotRequired { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

    public void PasswordHash()
    {
    }
}

class AuthorizedUser : User
{

}

class Administrator : User
{

}

abstract class UserFactory
{
    public User Initialize(User user)
    {
        User u = CreateUser(user);
        u.PasswordHash();

        return u;
    }

    protected abstract User CreateUser(User user);
}

class AuthorizedUserFactory : UserFactory
{
    protected override User CreateUser(User user)
    {
        if (!user.TwoFactorNotRequired && !user.TwoFactorAuthentication)
            throw new UnauthorizedAccessException();

        return new AuthorizedUser()
        {
            Password = user.Password,
            TwoFactorAuthentication = user.TwoFactorAuthentication,
            TwoFactorNotRequired = user.TwoFactorNotRequired,
            IsAdmin = user.IsAdmin
        };
    }
}

class AdminFactory : UserFactory
{
    protected override User CreateUser(User user)
    {
        if (!user.IsAdmin || !user.TwoFactorNotRequired && !user.TwoFactorAuthentication)
            throw new UnauthorizedAccessException();

        return new Administrator()
        {
            Password = user.Password,
            TwoFactorAuthentication = user.TwoFactorAuthentication,
            TwoFactorNotRequired = user.TwoFactorNotRequired,
            IsAdmin = true
        };
    }
}