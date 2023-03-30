using Cosmos.System.Network.IPv4;

namespace Cosmos.Zarlo.Net;

public class TcpClient : Cosmos.System.Network.IPv4.TCP.TcpClient
{
    public TcpClient() : base(0)
    {
    }

    public TcpClient(int localPort) : base(localPort)
    {
    }
    
    public TcpClient(string dest, int destPort) : base(DNS.LookUp(dest), destPort)
    {
    }

    public TcpClient(Address dest, int destPort) : base(dest, destPort)
    {
    }

    public void Connect(Uri uri) => Connect(DNS.LookUp(uri), uri.Port);

    public byte[] Receive()
    {
        var i = new EndPoint(null, 0);
        return Receive(ref i);
    }
}