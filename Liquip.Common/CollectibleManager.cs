using System.Collections.Generic;

namespace Liquip.Common;

public static class CollectibleManager
{

    public static List<ICollectible> Items = new List<ICollectible>();

    public static void Remove(ICollectible collectible)
    {
        Items.Remove(collectible);
    }

    public static void Add(ICollectible collectible)
    {
        Items.Add(collectible);
    }

    public static void Collect()
    {
        foreach (var items in Items)
        {
            items.Collect();
        }
    }

}
