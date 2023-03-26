using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace Cosmos.Zarlo.Net;

public static class DNS
{
    public static readonly Address Server = new Address(1, 1, 1, 1);

    public static Address LookUp(Uri domain) => LookUp(domain.Host);
    
    public static Address LookUp(string domain)
    {
        using var xClient = new DnsClient();
        xClient.Connect(Server); //DNS Server address
            
        xClient.SendAsk(domain);

        return xClient.Receive(); //can set a timeout value
    }
}