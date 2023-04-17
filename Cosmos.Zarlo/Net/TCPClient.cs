using System.Net.Sockets;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;

namespace Cosmos.Zarlo.Net;

public class TcpClient : Cosmos.System.Network.IPv4.TCP.TcpClient
{
    private PortClaim _portClaim;


    public TcpClient() : this(PortMap.GetRandomPort(ProtocolType.Tcp))
    {
    }

    public TcpClient(int localPort) : this(PortMap.GetPort(ProtocolType.Tcp, (ushort)localPort))
    {
    }

    public TcpClient(Address address) : this(PortMap.GetRandomPort(address, ProtocolType.Tcp))
    {
    }

    public TcpClient(Address address, int localPort) : this(PortMap.GetPort(address, ProtocolType.Tcp, (ushort)localPort))
    {
    }

    private TcpClient(PortClaim portClaim)
    {
        _portClaim = portClaim;
        StateMachine = new Tcp(_portClaim.Port.Value, 0, _portClaim.Address, Address.Zero);
        // StateMachine.RxBuffer = new Queue<TCPPacket>(8);
        StateMachine.Status = Status.CLOSED;
    }

    private TcpClient(PortClaim portClaim, Address dest, int destPort) : this(portClaim)
    {
        StateMachine.RemoteEndPoint.Address = dest;
        StateMachine.RemoteEndPoint.Port = (ushort)destPort;
    }

    public TcpClient(string dest, int destPort) : this(Address.Zero, DNS.LookUp(dest), destPort)
    {
    }

    public TcpClient(Address local, string dest, int destPort) : this(local, DNS.LookUp(dest), destPort)
    {
    }

    public TcpClient(Address local, Address dest, int destPort) : this(PortMap.GetRandomPort(local, ProtocolType.Tcp), dest, destPort)
    {
    }

    public void Connect(Uri uri) {
        Connect(DNS.LookUp(uri), uri.Port);
    }

    public byte[] Receive()
    {
        var i = new EndPoint(null, 0);
        return Receive(ref i);
    }
}