using Uroflow.Application;
using Uroflow.Infrastructure;
using Uroflow.Persistance;
using Core.Extensions.SystemExtensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.ConfigureCustomApplicationBuilder(typeof(Program).Assembly);
var app = builder.Build();
app.ConfigureCustomApplication();
app.UseStaticFiles();
app.Run();
