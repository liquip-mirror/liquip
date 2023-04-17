using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace Cosmos.Zarlo.Net;

public class DnsClient: IDisposable {
    
    private UdpClient UdpClient;
    private Address? Destination;
    public DnsClient() {
        UdpClient = new UdpClient();
    }

    public void Connect(Address address, ushort port)
    {
        UdpClient.Connect(address, port);
        Destination = address;
    }

    /// <summary>
    /// Sends a DNS query for the given domain name string.
    /// </summary>
    /// <param name="url">The domain name string to query the DNS for.</param>
    public void SendAsk(string url)
    {
        Address source = IPConfig.FindNetwork(Destination);
        
        var askpacket = new DNSPacketAsk(source, Destination, url);
        UdpClient.Send(askpacket.RawData);
    }

    public void Dispose()
    {
        UdpClient?.Dispose();
    }
}