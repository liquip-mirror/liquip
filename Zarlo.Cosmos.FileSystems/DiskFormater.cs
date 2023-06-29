using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;

namespace Zarlo.Cosmos.FileSystems;

public enum PartitionTableType
{
    MBR,
    GPT
}

public class PartitionTable
{
    public PartitionTableType Type { get; set; }

    public List<PartitionTableItem> Partitions { get; set; }

    public class PartitionTableItem
    {
        public string? Format { get; set; }
        public ulong StartingSector { get; set; }
        public ulong? SectorCount { get; set; }
    }
}

public class DiskFormater
{
    private readonly Dictionary<string, IFormat> _formaters = new();

    public void AddFormater(string name, IFormat formater)
    {
        if (!_formaters.ContainsKey(name))
        {
            _formaters.Add(name, formater);
        }
    }

    public IFormat GetFormater(string name)
    {
        return _formaters[name];
    }

    public void FormatPartition(string name, Partition partition)
    {
        GetFormater(name).Format(partition);
    }


    public void FormatPartition(string name, ManagedPartition partition)
    {
        GetFormater(name).Format(partition);
    }

    public void FormatDisk(BlockDevice disk, PartitionTable table)
    {
        if (table.Type == PartitionTableType.MBR)
        {
            var mbr = new MBR(disk);
            mbr.CreateMBR(disk);
            byte i = 0;
            foreach (var partition in table.Partitions)
            {
                mbr.WritePartitionInformation(
                    new Partition(
                        disk,
                        partition.StartingSector,
                        partition.SectorCount ?? disk.BlockCount - partition.StartingSector
                    ),
                    i
                );

                i++;
            }
        }
    }
}
