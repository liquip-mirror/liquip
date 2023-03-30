using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.ELF;

public unsafe class ElfFile
{
    public Elf32Ehdr ElfHeader { get; set; }
    public List<Elf32Shdr> SectionHeaders { get; set; } = new List<Elf32Shdr>();
    public List<Elf32Rel> RelocationInformation { get; set; } = new List<Elf32Rel>();
    public List<Elf32Sym> Symbols { get; set; } = new List<Elf32Sym>();
    private List<uint> _stringTables = new List<uint>();

    public string ResolveName(Elf32Shdr section, uint offset, PointerStream stream)
    {
        var old = stream.Position;
        if (section.Type != SectionType.SymbolTable)
        {
            stream.Position = _stringTables[0] + offset;
        }
        else
        {
            stream.Position = _stringTables[1] + offset;
        }
        var reader = new BinaryReader(stream);
        var s = reader.ReadString();
        stream.Position = old;
        return s;
    }

    public ElfFile(PointerStream stream)
    {
        //load main file header
        ElfHeader = new Elf32Ehdr((Elf32_Ehdr*) stream.Ptr);

        //load section headers
        var header = (Elf32_Shdr*) (stream.Ptr + ElfHeader.Shoff);

        for (int i = 0; i < ElfHeader.Shnum; i++)
        {
            var x = new Elf32Shdr(&header[i]);
            if (x.Type == SectionType.StringTable) _stringTables.Add(x.Offset);
            SectionHeaders.Add(x);
        }

        //now we can load names into symbols and process sub data
        for (var index = 0; index < SectionHeaders.Count; index++)
        {
            var sectionHeader = SectionHeaders[index];
            sectionHeader.Name = ResolveName(sectionHeader, sectionHeader.NameOffset, stream);

            switch (sectionHeader.Type)
            {
                case SectionType.Relocation:
                    for (int i = 0; i < sectionHeader.Size / sectionHeader.Entsize; i++)
                    {
                        RelocationInformation.Add(new Elf32Rel(
                                (Elf32_Rel*) (stream.Ptr + sectionHeader.Offset + i * sectionHeader.Entsize))
                            {Section = index});
                    }

                    break;
                case SectionType.SymbolTable:
                    for (int i = 0; i < sectionHeader.Size / sectionHeader.Entsize; i++)
                    {
                        var x = new Elf32Sym(
                            (Elf32_Sym*) (stream.Ptr + sectionHeader.Offset + i * sectionHeader.Entsize));
                        x.Name = ResolveName(sectionHeader, x.NameOffset, stream);
                        Symbols.Add(x);
                    }

                    break;
            }
        }
    }
}