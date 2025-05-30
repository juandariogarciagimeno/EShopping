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
COPY ["Services/Discount/EShopping.Discount.Grpc/EShopping.Discount.Grpc.csproj", "Services/Discount/EShopping.Discount.Grpc/"]
COPY ["Shared/EShopping.Shared.BuildingBlocks/EShopping.Shared.BuildingBlocks.csproj", "Shared/EShopping.Shared.BuildingBlocks/"]
COPY ["Shared/EShopping.Shared.Utils/EShopping.Shared.Utils.csproj", "Shared/EShopping.Shared.Utils/"]
COPY ["Services/Discount/EShopping.Discount.Data/EShopping.Discount.Data.csproj", "Services/Discount/EShopping.Discount.Data/"]
RUN dotnet restore "./Services/Discount/EShopping.Discount.Grpc/EShopping.Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/EShopping.Discount.Grpc"
RUN dotnet build "./EShopping.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShopping.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopping.Discount.Grpc.dll", "--server.urls", "http://+:8080;https://+:8081"]