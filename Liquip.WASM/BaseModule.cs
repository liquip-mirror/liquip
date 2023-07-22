namespace Liquip.WASM;

public class BaseModule
{
    public Dictionary<string, object> Exports = new();

    private int _functionIndex;
    public Function[] functions;
    public List<Function> Functions = new();
    public Global[] globals;
    public List<Global> Globals = new();
    public Memory[] memory;
    public List<Memory> Memory = new();
    public readonly string Name;

    private readonly Parser _parser;

    private Function _startFunction;
    public readonly Store Store;
    public readonly List<Table> Tables = new();

    private readonly List<Type> _types = new();

    public BaseModule(string name, Store store)
    {
        Name = name;
        Store = store;
    }

    public BaseModule(string name, Store store, byte[] bytes)
    {
        Name = name;
        Store = store;
        _parser = new Parser(bytes, this);

        if (_parser.GetByte() != 0x00 || _parser.GetByte() != 0x61 || _parser.GetByte() != 0x73 ||
            _parser.GetByte() != 0x6D)
        {
            throw new Exception("Invalid magic number.");
        }

        var version = _parser.GetVersion();

        while (!_parser.Done())
        {
            var section = _parser.GetByte();
            switch (section)
            {
                case 0x00:
                    var sectionSize = _parser.GetUInt32();
                    _parser.Skip(sectionSize);
                    break;
                case 0x01:
                    loadTypes();
                    continue;
                case 0x02:
                    loadImports();
                    continue;
                case 0x03:
                    loadFunctions();
                    continue;
                case 0x04:
                    loadTables();
                    continue;
                case 0x05:
                    loadMemory();
                    continue;
                case 0x06:
                    loadGlobals();
                    continue;
                case 0x07:
                    loadExports();
                    continue;
                case 0x08:
                    loadStart();
                    continue;
                case 0x09:
                    loadElements();
                    continue;
                case 0x0A:
                    loadCode();
                    continue;
                case 0x0B:
                    loadData();
                    continue;
                default:
                    throw new Exception("Invalid module section ID: 0x" + section.ToString("X"));
            }
        }

        functions = Functions.ToArray();
        memory = Memory.ToArray();
        globals = Globals.ToArray();

        if (_startFunction != null)
        {
            Store.runtime.Call(_startFunction.GlobalIndex);
            while (Store.Step(1000))
            {
            }
        }
    }

    private void loadTypes()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint fType = 0; fType < vectorSize; fType++)
        {
            if (_parser.GetByte() != 0x60)
            {
                throw new Exception("Invalid function type.");
            }

            var pTypeLength = _parser.GetUInt32();
            var parameters = new byte[pTypeLength];
            for (uint pType = 0; pType < pTypeLength; pType++)
            {
                var valType = _parser.GetValType();
                parameters[pType] = valType;
            }

            var rTypeLength = _parser.GetUInt32();
            var results = new byte[rTypeLength];
            for (uint rType = 0; rType < rTypeLength; rType++)
            {
                var valType = _parser.GetValType();
                results[rType] = valType;
            }

            _types.Add(new Type(parameters, results));
        }
    }

    private void loadImports()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint import = 0; import < vectorSize; import++)
        {
            var mod = _parser.GetName();
            var nm = _parser.GetName();

            var type = _parser.GetByte();

            if (!Store.Modules.ContainsKey(mod))
            {
                throw new Exception("Import module not found: " + mod + "@" + nm);
            }

            if (!Store.Modules[mod].Exports.ContainsKey(nm))
            {
                var typeString = "unknown";
                switch (type)
                {
                    case 0x00:
                        typeString = "function";
                        break;
                    case 0x01:
                        typeString = "table";
                        break;
                    case 0x02:
                        typeString = "memory";
                        break;
                    case 0x03:
                        typeString = "global";
                        break;
                }

                throw new Exception("Import (" + typeString + ") name not found: " + mod + "@" + nm);
            }

            switch (type)
            {
                case 0x00: // x:typeidx => func x
                    var funcTypeIdx = (int)_parser.GetIndex();
                    if (funcTypeIdx >= _types.Count())
                    {
                        throw new Exception("Import function type does not exist: " + mod + "@" + nm + " " +
                                            _types[funcTypeIdx]);
                    }

                    if (!_types[funcTypeIdx].SameAs(((Function)Store.Modules[mod].Exports[nm]).Type))
                    {
                        throw new Exception("Import function type mismatch: " + mod + "@" + nm + " - " +
                                            _types[funcTypeIdx] + " != " +
                                            ((Function)Store.Modules[mod].Exports[nm]).Type);
                    }

                    Functions.Add((Function)Store.Modules[mod].Exports[nm]);
                    _functionIndex++;

                    break;
                case 0x01: // tt:tabletype => table tt
                    var t = _parser.GetTableType();
                    if (!t.CompatibleWith((Table)Store.Modules[mod].Exports[nm]))
                    {
                        throw new Exception("Import table type mismatch: " + mod + "@" + nm + " " + t + " != " +
                                            (Table)Store.Modules[mod].Exports[nm]);
                    }

                    Tables.Add((Table)Store.Modules[mod].Exports[nm]);
                    break;
                case 0x02: // mt:memtype => mem mt
                    var m = _parser.GetMemType();
                    if (!m.CompatibleWith((Memory)Store.Modules[mod].Exports[nm]))
                    {
                        throw new Exception("Import memory type mismatch: " + mod + "@" + nm + " " + m +
                                            " != " + (Memory)Store.Modules[mod].Exports[nm]);
                    }

                    Console.WriteLine("Memory import found.");
                    Memory.Add((Memory)Store.Modules[mod].Exports[nm]);
                    break;
                case 0x03: // gt:globaltype => global gt
                    byte gType;
                    bool mutable;
                    _parser.GetGlobalType(out gType, out mutable);

                    if (((Global)Store.Modules[mod].Exports[nm]).Type != gType)
                    {
                        throw new Exception("Import global type mismatch: " + mod + "@" + nm);
                    }

                    var global = (Global)Store.Modules[mod].Exports[nm];
                    global.SetName(mod + "." + nm);
                    Globals.Add(global);
                    break;
                default:
                    throw new Exception("Invalid import type: 0x" + type.ToString("X"));
            }
        }
    }

    private void loadFunctions()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint function = 0; function < vectorSize; function++)
        {
            var index = (int)_parser.GetIndex();
            if (index < _types.Count())
            {
                Functions.Add(new Function(this, "$f" + (uint)Functions.Count(), _types[index]));
            }
            else
            {
                throw new Exception("Function type " + index + "/" + _types.Count() + " does not exist.");
            }
        }
    }

    private void loadTables()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint table = 0; table < vectorSize; table++)
        {
            Tables.Add(_parser.GetTableType());
        }
    }

    private void loadMemory()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint import = 0; import < vectorSize; import++)
        {
            Memory.Add(_parser.GetMemType());
        }
    }

    private void loadGlobals()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint import = 0; import < vectorSize; import++)
        {
            byte type;
            bool mutable;
            _parser.GetGlobalType(out type, out mutable);

            var f = new Function(this, "$loadGlobal" + import + "", new Type(null, new[] { type }));
            f.program = _parser.GetExpr();
            Store.runtime.Call(f.GlobalIndex);
            do
            {
            } while (Store.Step(1000));

            Globals.Add(new Global(type, mutable, Store.runtime.ReturnValue(), (uint)Globals.Count()));
        }
    }

    private void loadExports()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint export = 0; export < vectorSize; export++)
        {
            var nm = _parser.GetName();

            var type = _parser.GetByte();
            var index = (int)_parser.GetIndex();
            switch (type)
            {
                case 0x00: // x:typeidx => func x
                    if (index < Functions.Count())
                    {
                        Functions[index].SetName(nm);
                        Exports.Add(nm, Functions[index]);
                    }
                    else
                    {
                        throw new Exception("Function export \"" + nm + "\" index " + index + "/" +
                                            Functions.Count() + " does not exist.");
                    }

                    break;
                case 0x01: // tt:tabletype => table tt
                    if (index < Tables.Count())
                    {
                        Exports.Add(nm, Tables[index]);
                    }
                    else
                    {
                        throw new Exception("Table export \"" + nm + "\" index " + index + "/" + Tables.Count() +
                                            " does not exist.");
                    }

                    break;
                case 0x02: // mt:memtype => mem mt
                    if (index < Memory.Count())
                    {
                        Exports.Add(nm, Memory[index]);
                    }
                    else
                    {
                        throw new Exception("Memory export \" + nm + \" index " + index + "/" + Memory.Count() +
                                            " does not exist.");
                    }

                    break;
                case 0x03: // gt:globaltype => global gt
                    if (index < Globals.Count())
                    {
                        Globals[index].SetName(Name + "." + nm);
                        Exports.Add(nm, Globals[index]);
                    }
                    else
                    {
                        throw new Exception("Global export \"" + nm + "\" index " + index + "/" + Globals.Count() +
                                            " does not exist.");
                    }

                    break;
                default:
                    throw new Exception("Invalid export type: 0x" + type.ToString("X"));
            }
        }
    }

    private void loadStart()
    {
        var sectionSize = _parser.GetUInt32();
        var index = (int)_parser.GetIndex();

        if (index < Functions.Count())
        {
            _startFunction = Functions[index];
        }
        else
        {
            throw new Exception("Memory export \" + nm + \" index does not exist.");
        }
    }

    private void loadElements()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint element = 0; element < vectorSize; element++)
        {
            var tableidx = (int)_parser.GetUInt32();
            if (tableidx >= Tables.Count())
            {
                throw new Exception("Element table index does not exist");
            }

            var f = new Function(this, "$loadElement" + element, new Type(null, new[] { Type.i32 }));
            f.program = _parser.GetExpr();
            Store.runtime.Call(f.GlobalIndex);
            do
            {
            } while (Store.Step(1000));

            var offset = Store.runtime.ReturnValue().i32;

            var funcVecSize = _parser.GetUInt32();
            for (uint func = 0; func < funcVecSize; func++)
            {
                var funcidz = _parser.GetUInt32();

                Tables[tableidx].Set(offset + func, funcidz);
            }
        }
    }

    private void loadCode()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint funcidx = 0; funcidx < vectorSize; funcidx++)
        {
            if (funcidx >= Functions.Count())
            {
                throw new Exception("Missing function in code segment.");
            }

            var size = _parser.GetUInt32();
            var end = _parser.GetPointer() + size;
            var numLocals = _parser.GetUInt32();
            for (uint local = 0; local < numLocals; local++)
            {
                var count = _parser.GetUInt32();
                Functions[_functionIndex].LocalTypes = new Value[count];
                var type = _parser.GetValType();
                for (uint n = 0; n < count; n++)
                {
                    Functions[_functionIndex].LocalTypes[n].type = type;
                    Functions[_functionIndex].LocalTypes[n].i64 = 0;
                }
            }

            Functions[_functionIndex].program = _parser.GetExpr();
            _functionIndex++;

            if (_parser.GetPointer() != end)
            {
                throw new Exception("Invalid position: 0x" + _parser.GetPointer().ToString("X") +
                                    " after loading code.  Should be: 0x" + end.ToString("X") + " at 0x" +
                                    _parser.PeekByte());
            }
        }
    }

    private void loadData()
    {
        var sectionSize = _parser.GetUInt32();
        var vectorSize = _parser.GetUInt32();

        for (uint data = 0; data < vectorSize; data++)
        {
            var memidx = (int)_parser.GetUInt32();
            if (memidx >= Memory.Count())
            {
                throw new Exception("Data memory index does not exist");
            }

            var f = new Function(this, "loadData" + data, new Type(null, new[] { Type.i32 }));
            f.program = _parser.GetExpr();
            Store.runtime.Call(f.GlobalIndex);
            do
            {
            } while (Store.Step(1000));

            ulong offset = Store.runtime.ReturnValue().i32;
            var memVecSize = _parser.GetUInt32();
            Buffer.BlockCopy(_parser.GetBytes((int)_parser.GetPointer(), (int)memVecSize), 0, Memory[memidx].Buffer,
                (int)offset, (int)memVecSize);
            _parser.SetPointer(_parser.GetPointer() + memVecSize);
        }
    }

    /// <summary>
    /// expose a c# method so it can be used in WASM
    /// </summary>
    /// <param name="name">name of the export</param>
    /// <param name="parameters"></param>
    /// <param name="results"></param>
    /// <param name="action">the method</param>
    public void AddExportFunc(string name, byte[]? parameters = null, byte[]? results = null,
        Func<Value[], Value[]>? action = null)
    {
        if (parameters == null)
        {
            parameters = Array.Empty<byte>();
        }

        if (results == null)
        {
            results = Array.Empty<byte>();
        }

        Function func;

        if (action == null)
        {
            func = new Function(this, name, new Type(parameters, results));
        }
        else
        {
            func = new Function(this, name, action, new Type(parameters, results));
        }

        Exports.Add(name, func);
    }

    /// <summary>
    /// expose a c# void method so it can be used in WASM
    /// </summary>
    /// <param name="name"></param>
    /// <param name="f"></param>
    public void AddExportFunc(string name, Function f)
    {
        f.SetName(Name + "@" + name);

        Exports.Add(name, f);
    }

    public Global AddExportGlob(string name, byte type, bool mutable, Value v)
    {
        var global = new Global(type, mutable, v, (uint)Exports.Count());
        Exports.Add(name, global);
        return global;
    }

    public void AddExportMemory(string name, Memory m)
    {
        Exports.Add(name, m);
    }

    public void AddExportTable(string name, Table t)
    {
        Exports.Add(name, t);
    }

    public void DumpExports()
    {
        Console.WriteLine(Name + " exports:");
        foreach (var export in Exports)
        {
            var f = export.Value as Function;
            var t = export.Value as Table;
            var m = export.Value as Memory;

            if (f != null)
            {
                Console.WriteLine("Function " + export.Key + " " + f.Type);
            }
            else if (t != null)
            {
                Console.WriteLine("Table " + export.Key);
            }
            else if (m != null)
            {
                Console.WriteLine("Memory " + export.Key);
            }
            else
            {
                Console.WriteLine("Global " + export.Key);
            }
        }
    }

    public Value Call(string function, object[] parameters)
    {
        CallVoid(function, parameters);

        return Store.runtime.ReturnValue();
    }

    public void CallVoid(string function)
    {
        CallVoid(function, Array.Empty<object>());
    }

    public void CallVoid(string function, object[] parameters)
    {
        if (!Exports.ContainsKey(function) || Exports[function] as Function == null)
        {
            throw new Exception("Function \"" + function + "\" does not exist in " + Name + ".");
        }

        var f = Exports[function] as Function;

        Store.runtime.Call(f.GlobalIndex, parameters);
        Store.runtime.Step(999999999);


    }
}
