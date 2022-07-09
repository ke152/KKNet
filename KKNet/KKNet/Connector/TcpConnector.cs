using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KKNet.Connector;

public class TcpConnector : IConnector<Socket>
{
    private Socket socket;
    public event Action<Socket>? OnConnect;

    public TcpConnector()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public async Task ServerStart(int port)
    {
        this.socket.Bind(new IPEndPoint(IPAddress.Any, port));//TODO：ipEP可以考虑放到成员函数中
        this.socket.Listen();

        while (true)
        {
            Socket client = await this.socket.AcceptAsync();
            if (client != null)
            {
                this.OnConnect?.Invoke(client);
            }
        }
    }

    public Socket ClientStart(string ip, int port)
    {
        this.socket.Connect(ip, port);
        return this.socket;
    }
    public async Task<Socket> ClientStartAsync(string ip, int port)
    {
        await this.socket.ConnectAsync(ip, port);
        return this.socket;
    }

}
