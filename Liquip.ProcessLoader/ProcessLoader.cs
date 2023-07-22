﻿using Liquip.Common;
using Liquip.Threading;
using Liquip.Process.DotNetParser;

namespace Liquip;

public class StartOptions
{
    public string Exe { get; set; }
    public string WorkingDirectory { get; set; }
    public string[]? Args { get; set; }
    public object Env { get; set; }
}

public static class ProcessLoader
{

    public static Threading.Process? Start(StartOptions startOptions)
    {
        IProcess? process = null;
        using var file = new FileStream(startOptions.Exe, FileMode.Open);
        switch (file.ReadByte())
        {

            case 0xAD:
                switch (file.ReadByte())
                {
                    case 0x00:
                        if (file.ReadByte() != 0x61 || file.ReadByte() != 0x73 ||
                            file.ReadByte() != 0x6D)
                        {
                        }

                        break;
                    case 0x5A: // PE files
                        process = new DotNetParserProcess(startOptions.Exe);
                        break;
                }
                break;


        }

        if (process == null) return null;

        void run()
        {
            process.Main(startOptions.Args);
        }

        return new Threading.Process(run);
    }

}
