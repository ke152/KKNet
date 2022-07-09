using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KKNet.Session;

public interface ISession<T>
{
    public event Action<T> OnRecv;

    public void Send(T message);
}

