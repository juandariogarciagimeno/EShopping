using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace EShopping.Shared.Utils
{
    public static class JsonExtensions
    {
        public static bool TryDeserialize<T>(this string json, [NotNullWhen(true)] out T result)
        {
            try
            {
                var tmp = JsonSerializer.Deserialize<T>(json);
                if (tmp is null)
                {
                    result = default!;
                    return false;
                }

                result = tmp;
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }   
    }
}
