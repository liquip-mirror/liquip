using Zarlo.Cosmos.FileSystems.NTFS.Model.Enums;
using Zarlo.Cosmos.FileSystems.NTFS.Utility;

namespace Zarlo.Cosmos.FileSystems.NTFS.Model.Attributes;

public class AttributeIndexAllocation : Attribute
{
    public List<IndexAllocationChunk> Indexes { get; set; }
    public List<IndexEntry> Entries { get; set; }

    public override AttributeResidentAllow AllowedResidentStates => AttributeResidentAllow.NonResident;

    internal override void ParseAttributeNonResidentBody(Ntfs ntfsInfo)
    {
        var data = NtfsUtils.ReadFragments(ntfsInfo, NonResidentHeader.Fragments);

        var indexes = new List<IndexAllocationChunk>();
        var entries = new List<IndexEntry>();

        // Parse
        for (var i = 0; i < NonResidentHeader.Fragments.Count; i++)
        {
            for (var j = 0; j < NonResidentHeader.Fragments[i].Clusters; j++)
            {
                var offset =
                    (int)((NonResidentHeader.Fragments[i].StartingVCN -
                           NonResidentHeader.Fragments[0].StartingVCN) * ntfsInfo.BytesPerCluster +
                          j * ntfsInfo.BytesPerCluster);

                if (!IndexAllocationChunk.IsIndexAllocationChunk(data, offset))
                {
                    continue;
                }

                var index = IndexAllocationChunk.ParseBody(ntfsInfo, data, offset);

                indexes.Add(index);
                foreach (var f in index.Entries)
                {
                    entries.Add(f);
                }
            }
        }

        Indexes = indexes;
        Entries = entries;
    }
}
