using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KKNet.Connector;

public class TcpConnector : IConnector
{
    public event Action<Socket>? OnConnect;
}
