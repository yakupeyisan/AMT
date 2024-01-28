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

namespace Uroflow.Device.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        new Thread(() =>
        {
            SocketStart(stoppingToken);
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
    private async void SocketStart(CancellationToken stoppingToken)
    {
        HttpListener listener = null;
        try
        {

            // TcpListener'ý baþlat
            listener = new HttpListener();
            string url = "http://127.0.0.1:8080/";
            listener.Prefixes.Add(url);
            listener.Start();

            _logger.LogInformation($"Server listening on {url}", DateTimeOffset.Now);

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    _logger.LogInformation($"Client connected", DateTimeOffset.Now);
                    new Thread(() =>
                    {
                        _ = HandleWebSocketRequest(context, stoppingToken);
                    }).Start();
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }

                // Ýstemci ile iletiþim için ayrý bir iþ parçacýðý baþlat
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error: {ex.Message}", DateTimeOffset.Now);
        }
        finally
        {
            listener?.Stop();
        }
    }

    private async Task HandleWebSocketRequest(HttpListenerContext context,CancellationToken cancellationToken)
    {
        HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
        WebSocket webSocket = webSocketContext.WebSocket;
        _logger.LogInformation($"WebSocket connected: {context.Request.RemoteEndPoint}", DateTimeOffset.Now);

        byte[] buffer = new byte[4096];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue && !cancellationToken.IsCancellationRequested)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            _logger.LogInformation($"Received message: {receivedMessage}",DateTimeOffset.Now);
            var response = ProcessMessage(webSocket, receivedMessage);
            if (response != null) 
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        _logger.LogInformation($"WebSocket closed: {result.CloseStatusDescription}", DateTimeOffset.Now);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
    private async Task<string?> ProcessMessage(WebSocket webSocket,string message)
    {
        CommunicationModel typeData = JsonConvert.DeserializeObject<CommunicationModel>(message);
        if (typeData == null) return null;
        if(typeData.Type== CommunicationType.SetApplicationType.ToString())
        {
            var request = JsonConvert.DeserializeObject<DataCommunicationModel<string>>(message);
            if(request==null) return null;
            AppStorage.Applications.Add(request.Data,webSocket);
            typeData.Message = "Application type setted";
            return JsonConvert.SerializeObject(typeData);

        }
        return JsonConvert.SerializeObject(typeData);
    }
}

