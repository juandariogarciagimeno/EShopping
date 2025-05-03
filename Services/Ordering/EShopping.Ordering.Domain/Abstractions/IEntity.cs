﻿namespace EShopping.Ordering.Domain.Abstractions;

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
