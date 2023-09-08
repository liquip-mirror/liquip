using System;
using libDotNetClr;
using LibDotNetParser.CILApi;
using Liquip.Common;

namespace Liquip.Process.DotNetParser;

public class DotNetParserProcess: IProcess
{

    public static string FrameWorkPath { get; set; } = "0://framework/";

    public DotNetFile File;
    public DotNetClr Clr;

    public DotNetParserProcess(string exePath)
    {
        File = new DotNetFile(exePath);
        Clr = new DotNetClr(File, FrameWorkPath);
    }

    public void SetWorkingDir(string wd)
    {
        throw new NotImplementedException();
    }

    public void Environment(ProcessEnvironment env)
    {
        throw new NotImplementedException();
    }

    public int Main(string[] args)
    {
        Clr.Start(args);
        return 0;
    }
}
