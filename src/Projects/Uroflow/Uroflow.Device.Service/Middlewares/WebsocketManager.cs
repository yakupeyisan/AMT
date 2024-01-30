
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using Uroflow.Device.Service.Constants;
using Uroflow.Device.Service.Models;
using Newtonsoft.Json.Serialization;

namespace Uroflow.Device.Service.Middlewares;

public class WebsocketManager
{
    private JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented
    };
    private readonly ILogger<WebsocketManager> _logger;

    public WebsocketManager(ILogger<WebsocketManager> logger)
    {
        _logger = logger;
    }

    public async void SocketStart(CancellationToken stoppingToken)
    {
        HttpListener listener = null;
        try
        {

            // TcpListener'ı başlat
            listener = new HttpListener();
            string url = "http://127.0.0.1:8080/";
            listener.Prefixes.Add(url);
            listener.Start();

            _logger.LogInformation($"Server listening on {url}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
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
    private async Task HandleWebSocketRequest(HttpListenerContext context, CancellationToken cancellationToken)
    {
        HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
        WebSocket webSocket = webSocketContext.WebSocket;
        _logger.LogInformation($"WebSocket connected: {context.Request.RemoteEndPoint}", DateTimeOffset.Now);

        byte[] buffer = new byte[4096];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string applicationType = "";
        while (!result.CloseStatus.HasValue && !cancellationToken.IsCancellationRequested)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            _logger.LogInformation($"Received message: {receivedMessage}", DateTimeOffset.Now);
            await ProcessMessage(webSocket, receivedMessage);
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        _logger.LogInformation($"WebSocket closed: {result.CloseStatusDescription}", DateTimeOffset.Now);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
    private async Task SendMessage(WebSocket webSocket, string message)
    {
        try
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            var buffer = new ArraySegment<byte>(messageBytes);

            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error sending message: {ex.Message}", DateTimeOffset.Now);
        }
    }
    private async Task ProcessMessage(WebSocket webSocket, string message)
    {
        var typeData = JsonConvert.DeserializeObject<CommunicationModel>(message);
        if (typeData == null)
        {
            await Task.CompletedTask;
            return;
        }
        if (typeData.Type == CommunicationType.SetApplicationType.ToString())
        {
            await SetApplicationType(webSocket, typeData, message);
        }
        if (typeData.Type == CommunicationType.UpdateStart.ToString())
        {
            await UpdateStart();
        }
        if (typeData.Type == CommunicationType.UpdateEnd.ToString())
        {
            await UpdateEnd();
        }
        await Task.CompletedTask;
    }
    private async Task SetApplicationType(WebSocket webSocket, CommunicationModel typeData, string message)
    {
        var request = JsonConvert.DeserializeObject<DataCommunicationModel<string>>(message);
        if (request == null) return;
        if (!AppStorage.Applications.ContainsKey(request.Data))
            AppStorage.Applications.Add(request.Data, webSocket);
        AppStorage.Applications[request.Data] = webSocket;
        typeData.Message = "Application type setted";
        var result = JsonConvert.SerializeObject(typeData, Settings);
        await SendMessage(webSocket, result);
    }
    private async Task UpdateStart()
    {
        AppStorage.Applications.Keys.ToList().ForEach(async key =>
        {
            var socket = AppStorage.Applications[key];
            if (key == ApplicationType.Electron.ToString())
            {
                var obj = new { PageType = "static", Page = "update.html" };
                DataCommunicationModel<object> result = new();
                result.Type = "SetConfig";
                result.Data = obj;
                await SendMessage(socket, JsonConvert.SerializeObject(result, Settings));
            }
        });
        await Task.CompletedTask;
    }
    private async Task UpdateEnd()
    {
        AppStorage.Applications.Keys.ToList().ForEach(async key =>
        {
            var socket = AppStorage.Applications[key];
            if (key == ApplicationType.Electron.ToString())
            {
                var obj = new { PageType = "live", Page = "http://localhost:4200" };
                DataCommunicationModel<object> result = new();
                result.Type = "SetConfig";
                result.Data = obj;
                await SendMessage(socket, JsonConvert.SerializeObject(result, Settings));
            }
        });
        await Task.CompletedTask;
    }
}
