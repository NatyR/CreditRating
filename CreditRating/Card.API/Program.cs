using Card.API.Interfaces;
using Card.API.Services;
using Card.API.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHostedService<FileQueueWorker>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddScoped<RabbitService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.UseHttpsRedirection();

app.Run();
