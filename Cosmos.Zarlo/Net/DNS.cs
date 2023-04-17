using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace Cosmos.Zarlo.Net;

public static class DNS
{
    public static readonly Address[] Servers = new Address[4]{new Address(1, 1, 1, 1), new Address(8, 8, 8, 8), Address.Zero, Address.Zero};


    // replace with file at some point
    public static readonly Dictionary<string, Address> Hosts = new Dictionary<string, Address>();

    public static void AddRecord(string hostname, Address address)
    {
        if(Hosts.ContainsKey(hostname))
        {
            Hosts[hostname] = address;
        }
        Hosts.Add(hostname, address);
    }

    public static readonly Dictionary<string, (Address address, uint TTL)> Cache = new Dictionary<string, (Address address, uint TTL)>();

    public static Address? LookUp(Uri domain) => LookUp(domain.Host);

    public static Address? LookUp(string domain)
    {
        if(Cache.ContainsKey(domain))
        {
            var record = Cache[domain];
            return record.address;
        }
        try
        {
            return Address.Parse(domain);
        }
        catch (Exception)
        {
            
        }

        using var xClient = new DnsClient();
    
        foreach (var server in Servers)
        {
            if(server == null || server == Address.Zero) continue;
            try
            {
                xClient.Connect(server, 53); //DNS Server address

                xClient.SendAsk(domain);

                var o = xClient.Receive(); //can set a timeout value
                if(o == Address.Zero || o == null)
                    continue;

                xClient.Close();
                Cache.Add(domain, (o, 30));
                return o;
            }
            catch (Exception)
            {
                
            }
        
        }
    
        return null;
    
    }
}