using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uroflow.Device.Service.Models;

public class ElectronConfigModel
{
    public string PageType { get; set; }
    public string Page { get; set; }
}
public class CommunicationModel
{
    public string Type { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class DataCommunicationModel<T>: CommunicationModel
{
    public T Data { get; set; }
}
public enum ApplicationType
{
    Electron,
    Service,
    Angular
}
public enum CommunicationType
{
    SetApplicationType,
    GetConfig
}