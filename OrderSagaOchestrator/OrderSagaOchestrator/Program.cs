using AbstractedRabbitMQ.Setup;
using OrderSagaOchestrator.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRabbitMQConnection(x =>
{
    x.Url = "amqp://guest:guest@localhost:5672";
    x.ClientProvideName = "SagaOchestriator App";
}).AddRabbitMQPublisher(x =>
{
    x.exchange = "order-created";
    x.exchangeType = ExchangeType.Direct;
});
builder.Services.AddHttpClient<ICreateOrderService, CreateOrderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
