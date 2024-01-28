using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Uroflow.Device.Service.Constants;

public static class AppStorage
{
    public static IDictionary<string, WebSocket> Applications = new Dictionary<string,WebSocket>();
}
