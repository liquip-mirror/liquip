using IL2CPU.API.Attribs;

namespace Zarlo.Cosmos.Plugs.System;

[Plug(typeof(HashCode))]
public class HashCodeImpl
{
    private static uint? seed;

    public static uint GenerateGlobalSeed()
    {
        if (!seed.HasValue)
        {
            if (RDseed.IsSupported())
            {
                seed = (uint)RDseed.GetRDSeed32();
            }
            else
            {
                seed = 0;
            }
        }

        return seed.Value;
    }
}
