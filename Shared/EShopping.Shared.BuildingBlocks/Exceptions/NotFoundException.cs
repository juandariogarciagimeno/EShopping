﻿namespace EShopping.Shared.BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static NotFoundException Order(Guid id)
        {
            return new NotFoundException($"Order with id {id} not found.");
        }

        public static NotFoundException Product(Guid id)
        {
            return new NotFoundException($"Product with id {id} not found.");
        }

        public static NotFoundException Basket(string userName)
        {
            return new NotFoundException($"Basket for UserName {userName} not found.");
        }

        public static NotFoundException Create<TType, TKey>(TKey id)
        {
            return new NotFoundException($"Entity of type {typeof(TType).Name} with id {id} not found.");
        }
    }
}
