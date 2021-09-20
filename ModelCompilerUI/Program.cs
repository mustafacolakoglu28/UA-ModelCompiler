using ModelCompiler;
using System;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    private static void Main(string[] args)
    {
      bool noGui = Environment.CommandLine.Contains(consoleOutputCommandLineArgument);
      Program.Main(noGui);
    }

    private const string consoleOutputCommandLineArgument = "-console";
  }
}