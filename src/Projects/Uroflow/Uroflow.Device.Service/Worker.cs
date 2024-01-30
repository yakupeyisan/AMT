using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using System;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Uroflow.Device.Service.Models;
using static System.Net.Mime.MediaTypeNames;
using Uroflow.Device.Service.Constants;
using Newtonsoft.Json.Serialization;
using Uroflow.Device.Service.Middlewares;

namespace Uroflow.Device.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WebsocketManager _websocketManager;

    private JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented 
    };
    public Worker(ILogger<Worker> logger, WebsocketManager websocketManager)
    {
        _logger = logger;
        _websocketManager = websocketManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        new Thread(() =>
        {
            _websocketManager.SocketStart(stoppingToken);
        }).Start();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}

