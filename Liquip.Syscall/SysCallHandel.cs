namespace Liquip.Syscall;

public class SysCallHandelAttribute : Attribute
{
    
    public uint Code { get; set; }

    public SysCallHandelAttribute(uint code)
    {
        Code = code;
    }
}
