using System.Reflection;
using System.Runtime.InteropServices;

namespace PlugMaker;

public class LibraryImportFinder
{

    public static Assembly[] GetAssemblies()
    {
        var o = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in o)
        {
            Console.WriteLine(item.FullName);
        }
        Console.WriteLine();
        return o;
    }

    public static List<MethodInfo> Find()
    { 
        // (typeof(DllImportAttribute), false).Length > 0
        return GetAssemblies().SelectMany(i => i.GetTypes())
                .SelectMany(t => t.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static
                ))
                .Where(m => {
                    var f = m.GetMethodImplementationFlags();
                    if (
                        f.HasFlag(MethodImplAttributes.Native) || 
                        f.HasFlag(MethodImplAttributes.InternalCall) ||
                        (m.Attributes & MethodAttributes.PinvokeImpl) != 0
                        )
                    { 
                        return true;
                    }
                    else
                    { 
                        return false;
                    }
                })
                .ToList();

    }

}
