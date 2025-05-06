namespace EShopping.Web;

public static class DependencyContainer
{
    public static IServiceCollection AddRefitFor<TInterface>(this IServiceCollection services, IConfiguration config, string root)
        where TInterface : class
    {
        services
            .AddRefitClient<TInterface>()
            .ConfigureHttpClient(c =>
            {
                var uriBuilder = new UriBuilder(config["ApiSettings:GatewayAddress"]!);
                uriBuilder.Path = config[$"ApiSettings:{root}Root"]!;

                c.BaseAddress = uriBuilder.Uri;
            });

        return services;
    }
}
