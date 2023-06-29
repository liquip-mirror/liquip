using System.Reflection;
using Cosmos.Core;

namespace Zarlo.XSharp;

public static class Utils
{
    public static MethodBase GetMethodDef(Assembly aAssembly, string aType, string aMethodName, bool aErrorWhenNotFound)
    {
        var xType = aAssembly.GetType(aType, false);
        if (xType != null)
        {
            MethodBase xMethod = xType.GetMethod(aMethodName);
            if (xMethod != null)
            {
                return xMethod;
            }
        }

        if (aErrorWhenNotFound)
        {
            throw new Exception("Method '" + aType + "::" + aMethodName + "' not found!");
        }

        return null;
    }

    public static MethodBase GetMethodDef(Type aType, string aMethodName, bool aErrorWhenNotFound = true)
    {
        return GetMethodDef(aType.Assembly, aType.FullName, aMethodName, aErrorWhenNotFound);
    }

    public static MethodBase GetInterruptHandler(byte aInterrupt)
    {
        return GetMethodDef(typeof(INTs), "HandleInterrupt_" + aInterrupt.ToString("X2"), false);
    }
}
