using System;
using Liquip.Memory;
using Liquip.XSharp;

namespace Liquip.ELF;

public unsafe class Invoker
{
    private uint _offset;
    private uint eax, ebx, ecx, edx, esi, edi, esp, ebp;

    internal Invoker(Pointer stack)
    {
        Stack = stack;
    }

    public uint Offset
    {
        get => _offset;
        set => _offset = value;
    }

    public Pointer Stack { get; init; }

    public void Dump()
    {
        Console.WriteLine(
            $"eax:{eax}, ebx:{ebx}, ecx:{ecx}, edx:{edx}, esi:{esi}, edi:{edi}, esp: {esp}, ebp: {ebp}");
        for (var i = 0; i < 512; i++)
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

    public static void _CallCode(
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
    )
    {
        throw new ImplementedInPlugException();
    }
}
