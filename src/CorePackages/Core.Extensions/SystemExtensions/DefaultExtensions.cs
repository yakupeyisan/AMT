using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Extensions.OcelotExtensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Security.JWT;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Encryption;
using System.Text.Json.Serialization;
using System.Reflection;
using Core.Security;
using MediatR;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Newtonsoft.Json;
using Core.Utilities.IoC;

namespace Core.Extensions.SystemExtensions
{
    public static class DefaultExtensions
    {
        public static WebApplicationBuilder ConfigureCustomApplicationBuilder(this WebApplicationBuilder builder,Assembly assembly)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("general.settings.json")
                .Build();
            builder.Configuration.AddConfiguration(configuration);
            var origins = configuration.GetSection("AllowedHostList").Get<string[]>();
            builder.Services.AddCors(
                policy =>
                {
                    policy.AddDefaultPolicy(c =>
                    {
                        c.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
                });
            builder.Services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddConsulRegistration(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            }); 
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSecurityServices();

            builder.Services.AddValidatorsFromAssembly(assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            builder.Services.AddHttpContextAccessor();
            
            ServiceTool.Create(builder.Services);
            return builder;
        }
        public static WebApplication ConfigureCustomApplication(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ConfigureCustomExceptionMiddleware();

            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors();
            app.RegisterWithConsul(app.Lifetime, app.Configuration);

            return app;
        }
    }
}
