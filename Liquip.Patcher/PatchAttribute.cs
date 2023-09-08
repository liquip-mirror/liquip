using System;

namespace Liquip.Patcher;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class PatchAttribute: Attribute
{
    public PatchAttribute()
    {
    }

    public PatchAttribute(Type target)
    {
        Target = target ?? throw new ArgumentNullException(nameof(target));
    }

    public PatchAttribute(string targetName)
    {
        if (String.IsNullOrEmpty(targetName))
        {
            throw new ArgumentNullException(nameof(targetName));
        }

        TargetName = targetName;
    }

    public Type Target { get; set; }

    /// <summary>
    /// TargetName can be used to load private/internal classes. It has the format "[Class name], [Assembly]"
    /// </summary>
    public string TargetName { get; set; }

}
