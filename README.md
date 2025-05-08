# 🛒 EShopping Microservices Architecture (.NET 8)

An advanced, cloud-native e-commerce microservices solution built with ASP.NET Core 8, leveraging modern architectural patterns and observability tools.

---

## 📦 Overview

This project is a practical implementation of microservices architecture, inspired by the [Udemy course on Microservices Architecture and Implementation on .NET](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet). It encompasses:

- **Microservices**: Catalog, Basket, Discount, Ordering
- **API Gateway**: YARP Reverse Proxy
- **Frontend**: ASP.NET Core Razor Pages
- **Communication**: gRPC (sync), RabbitMQ with MassTransit (async)
- **Data Stores**: PostgreSQL (Marten), Redis, SQLite, SQL Server
- **Architecture**: CQRS, DDD, Clean Architecture, Vertical Slice
- **Observability**: Serilog, OpenTelemetry, Seq
- **Containerization**: Docker & Docker Compose

---

## 🧱 Microservices Breakdown

### 🛍️ Catalog Service
- **Framework**: ASP.NET Core Minimal APIs
- **Architecture**: Vertical Slice with Feature folders
- **Patterns**: CQRS using MediatR, Validation with FluentValidation
- **Data Store**: PostgreSQL with Marten (Document DB)
- **API Definition**: Carter library
- **Features**: Logging, Global Exception Handling, Health Checks

### 🧺 Basket Service
- **Framework**: ASP.NET Core Web API
- **Design Patterns**: Proxy, Decorator, Cache-Aside
- **Cache**: Redis
- **Inter-Service Communication**: Consumes Discount gRPC service
- **Messaging**: Publishes BasketCheckout events via MassTransit to RabbitMQ

### 🎁 Discount Service
- **Framework**: ASP.NET Core gRPC Server
- **Communication**: Exposes services using Protobuf messages
- **Data Store**: SQLite with Entity Framework Core
- **Features**: High-performance inter-service communication with Basket Service

### 📦 Ordering Service
- **Architecture**: DDD, CQRS, Clean Architecture
- **Patterns**: Domain Events, Integration Events
- **Data Store**: SQL Server with Entity Framework Core (Code-First)
- **Messaging**: Consumes BasketCheckout events via MassTransit from RabbitMQ

### 🌐 YARP API Gateway
- **Framework**: YARP Reverse Proxy
- **Features**: Gateway Routing Pattern, Rate Limiting with FixedWindowLimiter
- **Configuration**: Routes, Clusters, Paths, Transforms, Destinations

### 🖥️ WebUI ShoppingApp
- **Framework**: ASP.NET Core Razor Pages with Bootstrap 4
- **API Consumption**: Refit Library with Generated HttpClientFactory
- **Features**: View Components, Partial Views, Tag Helpers, Model Bindings, Validations

---

## 🔍 Observability Enhancements

Integrated **Serilog** and **OpenTelemetry** across all microservices, connected to a centralized **Seq** server for structured logging and distributed tracing.

- **Serilog**: Provides structured logging with enriched context.
- **OpenTelemetry**: Enables distributed tracing across services.
- **Seq**: Centralized log server accessible at `http://localhost:5341`.

> Ensure the Seq server is running via Docker Compose for full observability features.

---

## 🐳 Docker & Containerization

All services are containerized using Docker, orchestrated with Docker Compose for seamless development and deployment.

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Running the Application

```bash
git clone https://github.com/juandariogarciagimeno/EShopping.git
cd EShopping
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

### Access the Services
- **WebUI**: `http://localhost:5000`
- **Seq (Logs Dashboard)**: `http://localhost:5341`
- **RabbitMQ Management**: `http://localhost:15672` (default credentials: guest/guest)

> Some services may take a few moments to initialize. Monitor the logs for readiness.

---

## 🧪 Testing the Services

Use tools like [Postman](https://www.postman.com/) or `curl` to interact with the APIs exposed by the microservices via the YARP API Gateway.

```bash
curl http://localhost:5000/api/v1/catalog
```

---

## 📚 Resources
- [Udemy Course](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet)
- [Serilog](https://serilog.net/)
- [OpenTelemetry for .NET](https://opentelemetry.io/docs/instrumentation/net/)
- [Seq Logging Server](https://datalust.co/seq)

---

## 🤝 Contributing

Contributions are welcome! Fork the repository and submit a pull request for any enhancements or bug fixes.

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
