using System.IO;
using Liquip.Common;
using Liquip.DotNetParser;

namespace Zarlo.Cosmos;

public class StartOptions
{
    public string Exe { get; set; }
    public string WorkingDirectory { get; set; }
    public string[]? Args { get; set; }
    public object Env { get; set; }
}

public static class ProcessLoader
{

    public static Liquip.Threading.Process? Start(StartOptions startOptions)
    {
        IProcess? process = null;
        using var file = new FileStream(startOptions.Exe, FileMode.Open);
        byte[] header = new byte[10];
        file.Read(header);
        switch (header[0])
        {

            case 0xAD:
                switch (header[1])
                {
                    case 0x00:
                        if (
                            header[2] != 0x61 ||
                            header[3] != 0x73 ||
                            header[4] != 0x6D
                        )
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

        return new Liquip.Threading.Process(run);
    }

}
