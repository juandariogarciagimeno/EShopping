namespace EShopping.Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public static Customer Create(CustomerId id, string name, string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

        return new Customer()
        {
            Id = id,
            Name = name,
            Email = email
        };
    }

    public static Customer Create(string name, string email)
    {
        return Customer.Create(CustomerId.New(), name, email);
    }
}
