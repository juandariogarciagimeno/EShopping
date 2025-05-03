﻿namespace EShopping.Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }

    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        return new Product()
        {
            Id = id,
            Name = name,
            Price = price
        };
    }

    public static Product Create(string name, decimal price)
    {
        return Create(ProductId.New(), name, price);
    }
}
