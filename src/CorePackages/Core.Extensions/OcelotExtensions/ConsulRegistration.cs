using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models.ExternalConnectors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Core.Extensions.OcelotExtensions
{
    public class ConsulConfiguration
    {
        public string ServiceName { get; set; } = "";
        public string Address { get; set; } = "";
        public string[] Tags { get; set; } = new[] { "" };
    }
    public static class ConsulRegistration
    {
        public static IServiceCollection AddConsulRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration.GetConsulAddress() ?? "");
            }));
            return services;
        }
        public static void RegisterWithConsul(this IApplicationBuilder builder, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            var consulConfig = configuration.GetConsulConfiguration();
            if (consulConfig == null) return;
            var consulClient = builder.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggerFactory = builder.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();
            var uri = new Uri(configuration.GetConsulAddress() ?? "");
            var serviceUri = new Uri(consulConfig.Address);
            var registration = new AgentServiceRegistration()
            {
                ID = consulConfig.ServiceName,
                Name = consulConfig.ServiceName,
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                Tags = consulConfig.Tags,
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"{serviceUri.Scheme}://{serviceUri.Host}:{serviceUri.Port}/api/Health/Status",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            try
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                logger.LogInformation(message: $"Registering service with Consul, {consulConfig.ServiceName} on {uri.Host}:{uri.Port}");
                consulClient.Agent.ServiceRegister(registration).Wait();
                var kvPair = new KVPair("Services")
                {
                    Key= consulConfig.ServiceName,
                    Value = Encoding.UTF8.GetBytes($"\"{serviceUri.Scheme}://{serviceUri.Host}:{serviceUri.Port}\"")
                };
                consulClient.KV.Put(kvPair).Wait();

                lifetime.ApplicationStopping.Register(() =>
                {
                    logger.LogInformation(message: $"Deregistering service with Consul, {consulConfig.ServiceName} on {uri.Host}:{uri.Port}");
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                });
            }
            catch (Exception error)
            {
                logger.LogInformation(message: $"Error service with Consul, {consulConfig.ServiceName} on {uri.Host}:{uri.Port}, Error: {error.Message}");
            }
        }
        public static ConsulConfiguration? GetConsulConfiguration(this IConfiguration configuration)
        {
            return configuration.GetSection("ConsulConfig").Get<ConsulConfiguration>();
        }
        public static string? GetConsulAddress(this IConfiguration configuration)
        {

            return configuration.GetSection("ConsulAddress").Get<string>();
        }
        public static string? GetConsulValue( string key)
        {
            //Todo: refactor this to use the ConsulClient
            return "";
            //var consulConfig = ServiceTool.ServiceProvider.GetService<IConfiguration>().GetConsulConfiguration();
            //if (consulConfig == null) return null;
            //var consulClient = ServiceTool.ServiceProvider.GetRequiredService<IConsulClient>();
            //var kvPair = consulClient.KV.Get(key).Result.Response;
            //if (kvPair == null) return null;
            //return Encoding.UTF8.GetString(kvPair.Value);
        }

    }
}
