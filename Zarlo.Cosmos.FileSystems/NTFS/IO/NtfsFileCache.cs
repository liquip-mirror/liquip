using Zarlo.Cosmos.FileSystems.NTFS.Utility;

namespace Zarlo.Cosmos.FileSystems.NTFS.IO;

internal class NtfsFileCache
{
    private readonly CompatDictionary<ulong, NtfsFileEntry> _entries;

    internal NtfsFileCache()
    {
        _entries = new CompatDictionary<ulong, NtfsFileEntry>();
    }

    private ulong CreateKey(uint id, int filenameHashcode)
    {
        var key = (ulong)id << 32;

        if (filenameHashcode > 0)
        {
            key |= (ulong)filenameHashcode;
        }
        else
        {
            var tmp = (ulong)-filenameHashcode;
            tmp += (uint)1 << 31; // the 1-bit that's normally the sign bit
            key |= tmp;
        }

        return key;
    }

    public NtfsFileEntry Get(uint id, int filenameHashcode)
    {
        // Make combined key
        var key = CreateKey(id, filenameHashcode);

        // Fetch
        NtfsFileEntry tmp;
        _entries.TryGetValue(key, out tmp);

        if (tmp == null)
        {
            return null;
        }

        return tmp;
    }

    public void Set(uint id, ushort attributeId, NtfsFileEntry entry)
    {
        // Make combined key
        var key = CreateKey(id, attributeId);

        // Set
        _entries[key] = entry;
    }
}
