using System.Collections.ObjectModel;
using System.Text;
using Liquip.FileSystems.NTFS.Model.Attributes;
using Liquip.FileSystems.NTFS.Model.Enums;
using Liquip.FileSystems.NTFS.Utility;
using Attribute = Liquip.FileSystems.NTFS.Model.Attributes.Attribute;

namespace Liquip.FileSystems.NTFS.Model;

public class FileRecord
{
    private List<Attributes.Attribute> _attributes;
    private List<Attributes.Attribute> _externalAttributes;

    public string Signature { get; set; }
    public ushort OffsetToUSN { get; set; }
    public ushort USNSizeWords { get; set; }
    public ulong LogFileUSN { get; set; }
    public ushort SequenceNumber { get; set; }
    public short HardlinkCount { get; set; }
    public ushort OffsetToFirstAttribute { get; set; }
    public FileEntryFlags Flags { get; set; }
    public uint SizeOfFileRecord { get; set; }
    public uint SizeOfFileRecordAllocated { get; set; }
    public FileReference BaseFile { get; set; }
    public ushort NextFreeAttributeId { get; set; }
    public uint MFTNumber { get; set; }
    public byte[] USNNumber { get; set; }
    public byte[] USNData { get; set; }

    public FileReference FileReference { get; set; }

    public ReadOnlyCollection<Attributes.Attribute> Attributes => _attributes.AsReadOnly();

    public ReadOnlyCollection<Attributes.Attribute> ExternalAttributes => _externalAttributes.AsReadOnly();

    public bool IsExtensionRecord => BaseFile.RawId != 0;

    public static uint ParseAllocatedSize(byte[] data, int offset)
    {
        return BitConverter.ToUInt32(data, offset + 28);
    }

    public static FileRecord Parse(byte[] data, int offset, ushort bytesPrSector, uint sectors)
    {
        var length = bytesPrSector * sectors;
        // Debug.Assert(data.Length - offset >= length);
        // Debug.Assert(length >= 50);
        // Debug.Assert(bytesPrSector % 512 == 0 && bytesPrSector > 0);
        // Debug.Assert(sectors > 0);

        var res = new FileRecord();

        res.Signature = Encoding.ASCII.GetString(data, offset + 0, 4);
        // Debug.Assert(res.Signature == "FILE");

        res.OffsetToUSN = BitConverter.ToUInt16(data, offset + 4);
        res.USNSizeWords = BitConverter.ToUInt16(data, offset + 6);
        res.LogFileUSN = BitConverter.ToUInt64(data, offset + 8);
        res.SequenceNumber = BitConverter.ToUInt16(data, offset + 16);
        res.HardlinkCount = BitConverter.ToInt16(data, offset + 18);
        res.OffsetToFirstAttribute = BitConverter.ToUInt16(data, offset + 20);
        res.Flags = (FileEntryFlags)BitConverter.ToUInt16(data, offset + 22);
        res.SizeOfFileRecord = BitConverter.ToUInt32(data, offset + 24);
        res.SizeOfFileRecordAllocated = BitConverter.ToUInt32(data, offset + 28);
        res.BaseFile = new FileReference(BitConverter.ToUInt64(data, offset + 32));
        res.NextFreeAttributeId = BitConverter.ToUInt16(data, offset + 40);
        // Two unused bytes here
        res.MFTNumber = BitConverter.ToUInt32(data, offset + 44);

        res.USNNumber = new byte[2];
        Array.Copy(data, offset + res.OffsetToUSN, res.USNNumber, 0, 2);

        // Debug.Assert(data.Length - offset >= res.OffsetToUSN + 2 + res.USNSizeWords * 2);

        res.USNData = new byte[res.USNSizeWords * 2 - 2];
        Array.Copy(data, offset + res.OffsetToUSN + 2, res.USNData, 0, res.USNData.Length);

        res.FileReference = new FileReference(res.MFTNumber, res.SequenceNumber);

        // Apply the USN Patch
        NtfsUtils.ApplyUSNPatch(data, offset, sectors, bytesPrSector, res.USNNumber, res.USNData);

        res._attributes = new List<Attributes.Attribute>();
        res._externalAttributes = new List<Attributes.Attribute>();

        // Parse attributes
        res.ParseAttributes(data, res.SizeOfFileRecord - res.OffsetToFirstAttribute,
            offset + res.OffsetToFirstAttribute);

        return res;
    }

    private void ParseAttributes(byte[] data, uint maxLength, int offset)
    {
        // Debug.Assert(Signature == "FILE");

        var attribOffset = offset;
        for (var attribId = 0;; attribId++)
        {
            var attributeType = Model.Attributes.Attribute.GetType(data, attribOffset);
            if (attributeType == AttributeType.EndOfAttributes)
            {
                break;
            }

            uint length = Model.Attributes.Attribute.GetTotalLength(data, attribOffset);

            // Debug.Assert(attribOffset + length <= offset + maxLength);

            var attrib = Model.Attributes.Attribute.ParseSingleAttribute(data, (int)length, attribOffset);
            attrib.OwningRecord = FileReference;
            _attributes.Add(attrib);

            attribOffset += attrib.TotalLength;
        }
    }

    public void ParseExternalAttributes(FileRecord record)
    {
        foreach (var att in Attributes)
        {
            if (!(att is AttributeList list))
            {
                continue;
            }

            foreach (var otherAtt in record.Attributes)
            foreach (var itm in list.Items)
            {
                if (itm.AttributeId == otherAtt.Id && itm.BaseFile == otherAtt.OwningRecord)
                {
                    _externalAttributes.Add(otherAtt);
                    break;
                }
            }
        }
    }
}
