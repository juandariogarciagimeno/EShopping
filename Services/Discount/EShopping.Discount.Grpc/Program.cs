using EShopping.Discount.Grpc.Services;
using EShopping.Discount.Data;
using EShopping.Shared.Utils;
using EShopping.Shared.Utils.Tracing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddTraceLogger();

builder.UseSerilog();
builder.AddOpenTelemetry();

var app = builder.Build();

app.UseTraceLogger();

app.UseMigration();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
