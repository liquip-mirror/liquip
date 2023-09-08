using System;
using System.IO;
using Cosmos.Core.Memory;
using Liquip.Memory;

namespace Liquip.ELF;

public unsafe class UnmanagedExecutable
{
    private readonly ElfFile _elf;
    private byte* _finalExecutable;
    private readonly PointerStream _stream;

    public UnmanagedExecutable(PointerStream elfbin)
    {
        _stream = elfbin;
        _elf = new ElfFile(elfbin);
    }

    public void Load()
    {
        /*
         * 1. determin the total size of the final loaded sections
         * 2. maloc some space for them and allocate them
         * 3. update headers location information
         */

        //calcualte bss secstion size
        for (var i = 0; i < _elf.SectionHeaders.Count; i++)
        {
            var header = _elf.SectionHeaders[i];
            if (header.Type == SectionType.NotPresentInFile)
            {
                uint bssbase = 0;
                for (var index = 0; index < _elf.Symbols.Count; index++)
                {
                    var sym = _elf.Symbols[index];
                    if (sym.Shndx == 0xFFF2)
                    {
                        var size = sym.Value;
                        sym.Value = bssbase;
                        bssbase += size;
                        sym.Shndx = (ushort)i;
                    }
                }

                header.Size = bssbase;
                break;
            }
        }

        //calcualte final size
        uint totalSize = 0;
        foreach (var header in _elf.SectionHeaders)
        {
            if ((header.Flag & SectionAttributes.Alloc) == SectionAttributes.Alloc)
            {
                totalSize += header.Size;
            }
        }

        //alocate final size
        _finalExecutable = Heap.Alloc(totalSize);

        var stream = new UnmanagedMemoryStream(_finalExecutable, totalSize);
        var writer = new BinaryWriter(stream);

        foreach (var header in _elf.SectionHeaders)
        {
            if ((header.Flag & SectionAttributes.Alloc) != SectionAttributes.Alloc)
            {
                continue;
            }

            if (header.Type == SectionType.NotPresentInFile)
            {
                //update the meta data
                header.Offset = (uint)writer.BaseStream.Position;

                for (var i = 0; i < header.Size; i++)
                {
                    writer.Write((byte)0);
                }
            }
            else
            {
                //read the data from the orginal file
                var reader = new BinaryReader(_stream);
                reader.BaseStream.Position = header.Offset;

                //update the meta data
                header.Offset = (uint)writer.BaseStream.Position;

                //write the data from the old file into the loaded executible
                for (var i = 0; i < header.Size; i++)
                {
                    writer.Write(reader.ReadByte());
                }
            }
        }
    }


    public void Link()
    {
        foreach (var rel in _elf.RelocationInformation)
        {
            var symval = _elf.Symbols[(int)rel.Symbol].Value;

            var addr = (uint)_finalExecutable +
                       _elf.SectionHeaders[(int)_elf.SectionHeaders[rel.Section].Info].Offset;
            var refr = (uint*)(addr + rel.Offset);

            var memOffset = (uint)_finalExecutable +
                            _elf.SectionHeaders[_elf.Symbols[(int)rel.Symbol].Shndx].Offset;

            switch (rel.Type)
            {
                case RelocationType.R38632:
                    *refr = symval + *refr + memOffset; // Symbol + Offset
                    break;
                case RelocationType.R386Pc32:
                    *refr = symval + *refr - (uint)refr + memOffset; // Symbol + Offset - Section Offset
                    break;
                case RelocationType.R386None:
                    //nop
                    break;
                default:
                    Console.WriteLine($"Error RelocationType({(int)rel.Type}) not implemented");
                    break;
            }
        }
    }

    public Pointer Invoke(string function, uint stackSize, params object[] args)
    {
        var stack = Pointer.New(stackSize);
        var aw = new ArgumentWriter(stack);
        return Invoke(function, new Invoker(stack));
    }

    public Pointer Invoke(string function, uint stackSize)
    {
        return Invoke(function, new Invoker(Pointer.New(stackSize)));
    }

    public Pointer Invoke(string function, Pointer stack)
    {
        return Invoke(function, new Invoker(stack));
    }

    public Pointer Invoke(string function, Invoker invoker)
    {
        for (var i = 0; i < _elf.Symbols.Count; i++)
        {
            if (function == _elf.Symbols[i].Name)
            {
                invoker.Offset =
                    (uint)_finalExecutable + _elf.Symbols[i].Value +
                    _elf.SectionHeaders[_elf.Symbols[i].Shndx].Offset;

                invoker.CallCode();

                break;
            }
        }

        return invoker.Stack;
    }
}
