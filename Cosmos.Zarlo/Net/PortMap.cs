using System.Net.Sockets;
using Cosmos.System.Network.IPv4;
using Cosmos.Zarlo.Net.Exceptions;

namespace Cosmos.Zarlo.Net;


public class PortClaim: IDisposable
{
    private readonly ProtocolType _protocol;
    private readonly ushort? _port;
    private readonly string? _unixSocket;
    private readonly Address? _address;


    public ushort? Port => _port;
    public ProtocolType Protocol => _protocol;
    public Address? Address => _address;

    public string? UnixSocket => _unixSocket;

    internal PortClaim(ProtocolType protocol, string unixSocket)
    {
        _unixSocket = unixSocket;
        _protocol = protocol;
    }

    internal PortClaim(Address address, ProtocolType protocol, ushort port)
    {
        _address = address;
        _protocol = protocol;
        _port = port;
    }

    public void Dispose()
    {
        if(_port.HasValue)
            PortMap.Ports[_protocol][_address].Remove(_port.Value);
        if(!string.IsNullOrEmpty(_unixSocket))
            PortMap.UnixSockets.Remove(_unixSocket);
    }
}


public class PortMap {

    static Random rnd = new Random(RDseed.GetRDSeed32());

    public static PortClaim GetRandomPort(ProtocolType protocol) => GetRandomPort(Address.Zero, protocol);


    public static PortClaim GetRandomPort(Address address, ProtocolType protocol)
    {
        var output = (ushort)rnd.Next();
        var range = OutboundPortRange[protocol];
        while(true)
        {
            if(output >= range.min && output <= range.max)
            {
                if(!Ports[protocol].ContainsKey(address))
                    Ports[protocol] = new Dictionary<Address, HashSet<ushort>>();
                if(!Ports[protocol][address].Contains(output))
                {
                    Ports[protocol][address].Add(output);
                    return new PortClaim(address , protocol, output);
                }
            }
            output = (ushort)rnd.Next();
        }
        
    }

    
    public static PortClaim GetPort(ProtocolType protocol, ushort port) => GetPort(Address.Zero, protocol, port);

    public static PortClaim GetPort(Address address, ProtocolType protocol, ushort port)
    {

        if(!Ports[protocol].ContainsKey(address))
        {
            Ports[protocol] = new Dictionary<Address, HashSet<ushort>>();
        }
    
        if(!Ports[protocol][address].Contains(port))
        {
            Ports[protocol][address].Add(port);
            return new PortClaim(address , protocol, port);
        }
        else
        {
            throw new PortInUseException(protocol, port);
        }

        throw new PortInUseException(protocol, port);
        
        
    }

    public static PortClaim GetUnixSocket(ProtocolType protocol, string path)
    {

        if(!UnixSockets.Contains(path))
        {
                UnixSockets.Add(path);
                return new PortClaim(protocol, path);
        }
        else
        {
            throw new PortInUseException(ProtocolType.Unspecified, 0);
        }
        
    }

    static Dictionary<ProtocolType, (short min, short max)> OutboundPortRange = new Dictionary<ProtocolType, (short min, short max)>();


    internal static HashSet<string> UnixSockets = new HashSet<string>();

    internal static Dictionary<ProtocolType, Dictionary<Address, HashSet<ushort>>> Ports = new Dictionary<ProtocolType, Dictionary<Address, HashSet<ushort>>>();

}