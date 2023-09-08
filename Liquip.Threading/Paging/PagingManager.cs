using IL2CPU.API.Attribs;
using Liquip.Memory;
using Liquip.Threading.Paging.Struct;
using Liquip.XSharp;
using Liquip.XSharp.Fluent;using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using static XSharp.XSRegisters;

namespace Liquip.Threading.Paging;

/// <summary>
/// manages the page system
/// </summary>
public class PagingManager
{

    public static readonly PagingDirectory Directory = new PagingDirectory();

    public static readonly ProcessStatic<PagingDirectory> Current = new ProcessStatic<PagingDirectory>();

    public static void EnablePaging(uint pageDirectory) => throw new ImplementedInPlugException();


}

public class EnablePagingAsm : AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        var args = ArgumentBuilder.New();
        args.Add<uint>("pageDirectory");
        FluentXSharp.NewX86()
            .Set(CR3, args.GetArg("pageDirectory"))
            .Set(EAX, CR0)
            .Or(EAX, 0x80000000)
            .Set(CR0, EAX);
    }
}

public class PagingDirectory
{

    public Pointer ptr = Pointer.New(1042 * 4);

    public Page[] Pages = new Page[1024];

    public void AddEntry(uint index, uint value)
    {
        unsafe
        {
            ptr.Ptr[index] = value;
        }
    }

    public void AddEntry(uint index, PagingTableEntry value)
        => AddEntry(index, value.Raw);


    public void MakeEntry(uint index, PagingTableEntry value, uint size)
    {
        AddEntry(index, value.Raw);
    }



    public PagingTableEntry GetEntry(uint index)
    {
        unsafe
        {
            return new PagingTableEntry { Raw = ptr.Ptr[index] };
        }
    }

}
