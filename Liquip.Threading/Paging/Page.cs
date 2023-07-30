using Liquip.Memory;

namespace Liquip.Threading.Paging;

/// <summary>
///
/// </summary>
public struct Page
{
    /// <summary>
    ///
    /// </summary>
    public Address VirtualAddress { get; set; }
    /// <summary>
    ///
    /// </summary>
    public Address PhysicalAddress { get; set; }
    /// <summary>
    ///
    /// </summary>
    public ushort Permissions { get; set; }
    /// <summary>
    /// size of the page
    /// </summary>
    public ulong Size { get; set; }
}
