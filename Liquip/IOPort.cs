using PORT = Cosmos.Core.IOPort;

namespace Liquip;

/// <summary>
///     an object oriented IO Port class to lower the changes of typos.
/// </summary>
public class OIOPort
{
    private readonly int _port;

    public OIOPort(uint port)
    {
        _port = (int)port;
    }

    /// <summary>
    ///     Write byte to port.
    ///     Plugged.
    /// </summary>
    /// <param name="aData">A data.</param>
    public void Write8(byte aData)
    {
        PORT.Write8(_port, aData);
    }

    /// <summary>
    ///     Write Word to port.
    ///     Plugged.
    /// </summary>
    /// <param name="aPort">A port to write to.</param>
    /// <param name="aData">A data.</param>
    public void Write16(ushort aData)
    {
        PORT.Write16(_port, aData);
    }

    /// <summary>
    ///     Write DWord to port.
    ///     Plugged.
    /// </summary>
    /// <param name="aData">A data.</param>
    public void Write32(uint aData)
    {
        PORT.Write32(_port, aData);
    }

    /// <summary>
    ///     Read byte from port.
    /// </summary>
    /// <returns>byte value.</returns>
    public byte Read8()
    {
        return PORT.Read8(_port);
    }

    /// <summary>
    ///     Read Word from port.
    /// </summary>
    /// <returns>ushort value.</returns>
    public ushort Read16()
    {
        return PORT.Read16(_port);
    }

    /// <summary>
    ///     Read DWord from port.
    /// </summary>
    /// <returns>uint value.</returns>
    public uint Read32()
    {
        return PORT.Read32(_port);
    }

    /// <summary>Read byte from base port.</summary>
    /// <param name="aData">Output data array.</param>
    /// <exception cref="T:System.OverflowException">Thrown if aData lenght is greater than Int32.MaxValue.</exception>
    public void Read8(byte[] aData)
    {
        PORT.Read8(_port, aData);
    }

    /// <summary>Read Word from base port.</summary>
    /// <param name="aData">Output data array.</param>
    /// <exception cref="T:System.OverflowException">Thrown if aData lenght is greater than Int32.MaxValue.</exception>
    public void Read16(ushort[] aData)
    {
        PORT.Read16(_port, aData);
    }

    /// <summary>Read DWord from base port.</summary>
    /// <param name="aData">Output data array.</param>
    /// <exception cref="T:System.OverflowException">Thrown if aData lenght is greater than Int32.MaxValue.</exception>
    public void Read32(uint[] aData)
    {
        PORT.Read32(_port, aData);
    }

    /// <summary>Wait for the previous IO read/write to complete.</summary>
    public void Wait()
    {
        PORT.Wait();
    }
}
