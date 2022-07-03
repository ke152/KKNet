using KKNet;

string localIP = "127.0.0.1";
int localPort = 18000;

Console.WriteLine("input:  1 as server; 2 as client;");
string line = Console.ReadLine();

int flag = 0;
if (!int.TryParse(line, out flag))
    return;

KKSocket kkSocket = new();
switch (flag)
{
    case 1:
        kkSocket.ServerStart(localIP, localPort);
        break;
    case 2:
        kkSocket.ClientStart(localIP, localPort);
        break;
    default:
        Console.WriteLine("input:  1 as server; 2 as client;");
        break;
}
System.Threading.Thread.Sleep(10000);
