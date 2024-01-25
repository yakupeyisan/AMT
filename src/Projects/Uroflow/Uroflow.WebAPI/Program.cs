using Uroflow.Application;
using Uroflow.Infrastructure;
using Uroflow.Persistance;
using Core.Extensions.SystemExtensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.ConfigureCustomApplicationBuilder(typeof(Program).Assembly);
var app = builder.Build();
app.ConfigureCustomApplication();
app.UseStaticFiles();
app.Run();
