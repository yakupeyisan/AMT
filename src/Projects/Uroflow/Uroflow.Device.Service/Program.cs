using Uroflow.Device.Service;
using Uroflow.Device.Service.Middlewares;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<WebsocketManager>();

var host = builder.Build();
host.Run();
