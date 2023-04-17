using System.Net.Sockets;
using Cosmos.System.Network.IPv4.UDP;

namespace Cosmos.Zarlo.Net;


public class UdpClient : Cosmos.System.Network.IPv4.UDP.UdpClient
{
    private readonly PortClaim _portClaim;

    public UdpClient() : this(PortMap.GetRandomPort(ProtocolType.Udp))
    {

    }

    public UdpClient(int port) : this(PortMap.GetPort(ProtocolType.Udp, (ushort)port))
    {

    }

    private UdpClient(PortClaim portClaim)
    {
        _portClaim = portClaim;
    }

    public new void Dispose()
    {
        _portClaim.Dispose();
        base.Dispose();
    }

}