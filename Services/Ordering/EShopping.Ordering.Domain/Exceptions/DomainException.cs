using System.Diagnostics.CodeAnalysis;

namespace EShopping.Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base($"Domain Exception: '{message}' throws from Domain Layer.")
    {
    }

    public static void ThrowIfEmpty<T>(Guid g)
    {
        if (g == Guid.Empty)
            Throw("Value for {0} cannot be empty", nameof(T));
    }

    public static void ThrowIf(Func<bool> condition, string message)
    {
        if (condition.Invoke())
            Throw(message);
    }

    [DoesNotReturn]
    private static void Throw(string message, params object[] args)
    {
        throw new DomainException(string.Format(message, args));
    }
}
