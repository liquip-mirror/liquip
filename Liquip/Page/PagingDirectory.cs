using Liquip.Memory;

namespace Liquip.Page;

public struct PagingDirectory
{
}

public struct Page
{
    public Address VirtualAddress { get; set; }
    public Address PhysicalAddress { get; set; }
    public ushort Permissions { get; set; }
    public ulong Size { get; set; }
}


public struct TranslationLookasideBuffer
{
    public Address VirtualAddress { get; set; }
    public Address PhysicalAddress { get; set; }
    public ushort Permissions { get; set; }
}
