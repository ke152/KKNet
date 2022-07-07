using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KKNet.Connector;

public interface IConnector<T>
{
    event Action<T> OnConnect;
}

public interface IConnector : IConnector<Socket>
{

}

class TcpListener