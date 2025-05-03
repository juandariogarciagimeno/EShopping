# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/EShopping.Ordering.Api/EShopping.Ordering.Api.csproj", "Services/Ordering/EShopping.Ordering.Api/"]
COPY ["Shared/EShopping.Shared.BuildingBlocks/EShopping.Shared.BuildingBlocks.csproj", "Shared/EShopping.Shared.BuildingBlocks/"]
COPY ["Shared/EShopping.Shared.Utils/EShopping.Shared.Utils.csproj", "Shared/EShopping.Shared.Utils/"]
COPY ["Services/Ordering/EShopping.Ordering.Application/EShopping.Ordering.Application.csproj", "Services/Ordering/EShopping.Ordering.Application/"]
COPY ["Shared/EShopping.Shared.BuildingBlocks.Messaging/EShopping.Shared.BuildingBlocks.Messaging.csproj", "Shared/EShopping.Shared.BuildingBlocks.Messaging/"]
COPY ["Services/Ordering/EShopping.Ordering.Domain/EShopping.Ordering.Domain.csproj", "Services/Ordering/EShopping.Ordering.Domain/"]
COPY ["Services/Ordering/EShopping.Ordering.Infrastructure/EShopping.Ordering.Infrastructure.csproj", "Services/Ordering/EShopping.Ordering.Infrastructure/"]
RUN dotnet restore "./Services/Ordering/EShopping.Ordering.Api/EShopping.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/EShopping.Ordering.Api"
RUN dotnet build "./EShopping.Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShopping.Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopping.Ordering.Api.dll"]