using System.Text;
using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Plugs;

[Plug(Target = typeof(string))]
public static class StringPlug
{
    public static string Normalize(string aThis, NormalizationForm f) => aThis.ToLowerInvariant();
}