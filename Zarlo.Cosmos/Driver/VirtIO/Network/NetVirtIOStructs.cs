using System.Runtime.InteropServices;

namespace Zarlo.Cosmos.Driver.VirtIO.Network;

[StructLayout(LayoutKind.Auto)]
public struct NetHeader
{
    public byte Flags; // Bit 0: Needs checksum; Bit 1: Received packet has valid data;

    // Bit 2: If VIRTIO_NET_F_RSC_EXT was negotiated, the device processes
    // duplicated ACK segments, reports number of coalesced TCP segments in ChecksumStart
    // field and number of duplicated ACK segments in ChecksumOffset field,
    // and sets bit 2 in Flags(VIRTIO_NET_HDR_F_RSC_INFO) 
    public byte SegmentationOffload; // 0:None 1:TCPv4 3:UDP 4:TCPv6 0x80:ECN
    public ushort HeaderLength; // Size of header to be used during segmentation.
    public ushort SegmentLength; // Maximum segment size (not including header).
    public ushort ChecksumStart; // The position to begin calculating the checksum.
    public ushort ChecksumOffset; // The position after ChecksumStart to store the checksum.
    public ushort BufferCount; // Used when merging buffers.
}
