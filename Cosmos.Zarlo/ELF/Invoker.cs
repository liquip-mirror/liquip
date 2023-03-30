using Cosmos.Zarlo.Memory;
using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp;
using XSharp.Zarlo;
using XSharp.Zarlo.Fluent;


namespace Cosmos.Zarlo.ELF;

public unsafe class Invoker
{
    public uint Offset
    {
        get => _offset; 
        set => _offset = value; 
    }
    uint _offset;
    private uint eax, ebx, ecx, edx, esi, edi, esp, ebp;
    public Pointer Stack { get; init; }

    internal Invoker(Pointer stack)
    {
        Stack = stack;
    }

    public void Dump()
    {
        Console.WriteLine(
            $"eax:{eax}, ebx:{ebx}, ecx:{ecx}, edx:{edx}, esi:{esi}, edi:{edi}, esp: {esp}, ebp: {ebp}");
        for (int i = 0; i < 512; i++)
        {
            Console.Write(((byte*)Stack.Ptr)[i] + " ");
        }
    }

    public void CallCode()
    {
        var pointer = Stack.Ptr;
        _CallCode(
            ref _offset,
            ref pointer,
            ref eax,
            ref ebx,
            ref ecx,
            ref edx,
            ref esi,
            ref edi,
            ref esp,
            ref ebp
        );
    }

    public static unsafe void _CallCode(
        ref uint offset,
        ref uint* stack,
        ref uint eax,
        ref uint ebx,
        ref uint ecx,
        ref uint edx,
        ref uint esi,
        ref uint edi,
        ref uint esp,
        ref uint ebp
    ) => throw new ImplementedInPlugException();
    
}
