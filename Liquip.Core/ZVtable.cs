using Cosmos.Core;

namespace Liquip.Core;

public static class ZVtables
{
    public static int GetType(Type type)
    {
        return VTablesImpl.GetType(type.Name);
    }

    public static VTable GetVTable(Type type)
    {
        return VTablesImpl.mTypes[GetType(type)];
    }

    public static VTable GetVTable(uint aType)
    {
        return VTablesImpl.mTypes[aType];
    }
}
