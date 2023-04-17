using System.Net.Sockets;

namespace Cosmos.Zarlo.Net.Exceptions;


public class PortInUseException: NetworkException
{
    public PortInUseException(ProtocolType protocol, ushort port): base(string.Format("{0}: {1}", protocol, port))
    {

    }
}
