using System.Net;
using System.Net.Sockets;

namespace KKNet;

public class KKSocket
{
    private Socket skt;
    public KKSocket()
    {
        skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    #region Server
    /// <summary>
    /// socket accept in loop
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public void ServerStart(string ip, int port)
    {
        try
        {
            skt.Bind(new IPEndPoint(IPAddress.Parse(ip), port));//TODO：ipEP可以考虑放到成员函数中
            skt.Listen();

            SocketAsyncEventArgs eventArgs = new();//TODO：考虑加到EventPool中,微软文档应该有
            eventArgs.Completed += OnAccept;
            if (!skt.AcceptAsync(eventArgs))//TODO:这个accept怎么做多连接，异步的不能加死循环吧？
            {
                OnAccept(null, eventArgs);
            }
        }
        catch (Exception e)
        {
            //TODO:logging
        }
    }

    private void OnAccept(object? sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError != SocketError.Success)
        {
            Console.WriteLine("accept failed");
            return;
        }
        Console.WriteLine("KKSocket accept");
    }

    #endregion

    #region Client

    public void ClientStart(string remoteIP, int remotePort)
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        ClientStart(remoteEndPoint);
    }

    public void ClientStart(IPEndPoint remoteEndPoint)
    {
        try
        {
            SocketAsyncEventArgs eventArgs = new();//TODO：考虑加到EventPool中,微软文档应该有
            eventArgs.RemoteEndPoint = remoteEndPoint;
            eventArgs.Completed += OnConnect;
            skt.ConnectAsync(eventArgs);
            //TODO:recv
        }
        catch (Exception e)
        {
            //TODO:logging
        }
    }

    public void OnConnect(object? sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError != SocketError.Success)
        {
            Console.WriteLine("Connect to server failed");
            return;
        }
        Console.WriteLine("Connect to server succeed");
        //TODO:register or send data
    }

    public void Clos()
    {
        this.skt?.Close();
    }

    #endregion
    /*
    public void Close()
    {
        if (skt != null)
        {
            skt.Close();
        }
    }
    */

}
